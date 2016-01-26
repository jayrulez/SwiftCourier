$(document).ready(function () {
    var GCT_RATE = 0.165;
    var BASE_WEIGHT = 0;
    var COST_PER_UNIT = 0;

    $.ajax({
        cache: false,
        dataType: 'json',
        data: { 'name': 'base_weight' },
        type: 'GET',
        url: '/api/settings',
        success: function (data) {
            if (data) {
                BASE_WEIGHT = data.Value;
            }
        },
        error: function () {
        }
    });

    $.ajax({
        cache: false,
        dataType: 'json',
        data: { 'name': 'cost_per_unit' },
        type: 'GET',
        url: '/api/settings',
        success: function (data) {
            if (data) {
                COST_PER_UNIT = data.Value;
            }
        },
        error: function () {
        }
    });

    console.log(BASE_WEIGHT);
    console.log(COST_PER_UNIT);

    $('#Invoice_ServiceCost').attr('readonly', true);
    $('#Invoice_GCT').attr('readonly', true);
    $('#Invoice_Total').attr('readonly', true);

    $('.checkbo').checkBo();

    if ($('#PickupRequired').is(':checked')) {
        $('#PickupAddress').parents('div[class="form-group"]').show();
        $('#PickupContactNumber').parents('div[class="form-group"]').show();
    } else {
        $('#PickupAddress').parents('div[class="form-group"]').hide();
        $('#PickupContactNumber').parents('div[class="form-group"]').hide();
    }

    $('#PickupRequired').on('change', function () {

        $('#CustomerId').trigger('change');

        if ($(this).is(':checked')) {
            $('#PickupAddress').parents('div[class="form-group"]').show();
            $('#PickupContactNumber').parents('div[class="form-group"]').show();
        } else {
            $('#PickupAddress').parents('div[class="form-group"]').hide();
            $('#PickupContactNumber').parents('div[class="form-group"]').hide();
        }
    });

    $('#CustomerId').select2({
        placeholder: "Search for a customer",
        minimumInputLength: 1,
        ajax: {
            url: "/api/customers",
            dataType: 'json',
            quietMillis: 250,
            data: function (term, page) {
                return {
                    q: term,
                };
            },
            results: function (data, page) {
                return { results: data.items };
            },
            cache: true
        },
        initSelection: function (element, callback) {
            var id = $(element).val();
            if (id != "" && id != 0) {
                console.log(id);

                $.ajax("/api/customer/" + id, {
                    dataType: "json"
                }).done(function (data) { callback(data); });
            }
        },
        id: function (data) { return data.Id },
        formatResult: function (data) {
            return data.Name;
        },
        formatSelection: function (data) {
            return data.Name;
        },
        dropdownCssClass: "bigdrop",
        escapeMarkup: function (m) { return m; }
    });

    $('#CustomerId').on('change', function (e) {
        var customerId = $(this).val();
        var pickupRequired = $('#PickupRequired').val();

        if (customerId) {
            $.ajax({
                cache: false,
                dataType: 'json',
                type: 'GET',
                url: '/api/customer/' + customerId,
                success: function (data) {
                    if (data) {
                        if (pickupRequired) {
                            $('#PickupAddress').val(data.Address);
                            $('#PickupContactNumber').val(data.ContactNumber);
                        } else {
                            $('#PickupAddress').val('');
                            $('#PickupContactNumber').val('');
                        }

                        if (data.TaxExempted) {
                            GCT_RATE = 0.00;
                        } else {
                            GCT_RATE = 0.165;
                        }
                    } else {
                        GCT_RATE = 0.165;
                    }
                },
                error: function () {
                    GCT_RATE = 0.165;
                },
                complete: function () {
                    console.log(GCT_RATE);
                    $('#ServiceId').trigger('change');
                }
            });
        }
    });

    $('#Invoice_DiscountType').on("change", function (e) {
        $('#Invoice_DiscountValue').val(0).trigger("change");
        //$('#ServiceId').trigger("change");
    });

    $('#Invoice_DiscountValue').on("change", function (e) {
        if ($(this).val()) {
            var discountType = $('#Invoice_DiscountType').val();

            if (discountType == 0) {
                $('#Invoice_DiscountAmount').val(0).trigger("change");
            }

            if (discountType == 1) {
                var rate = parseFloat($(this).val());

                var discountAmount = parseFloat($('#Invoice_ServiceCost').val()) * (rate / 100);

                $('#Invoice_DiscountAmount').val(discountAmount).trigger("change");
            }

            if (discountType == 2) {
                $('#Invoice_DiscountAmount').val($(this).val()).trigger("change");
            }
        }
    });

    $('#Package_Weight').on("change", function (e) {
        var additionalWeight = parseFloat($(this).val()) - BASE_WEIGHT;
        if (additionalWeight < 0) {
            additionalWeight = 0;
        }

        var additionalCost = additionalWeight * COST_PER_UNIT;

        var serviceCost = parseFloat($('#Invoice_ServiceCost').val());
        if (serviceCost) {
            $('#Invoice_ServiceCost').val(serviceCost + additionalCost).trigger("change");
        }
    });

    $('#Invoice_DiscountAmount').on("change", function (e) {
        var discountAmount = parseFloat($(this).val());
        if (discountAmount) {
            var serviceCost = parseFloat($('#Invoice_ServiceCost').val());

            $('#Invoice_ServiceCost').val(serviceCost - discountAmount).trigger("change");
        }
    });

    $('#Invoice_ServiceCost').on("change", function (e) {
        var cost = parseFloat($(this).val());
        if (cost || cost == 0) {
            var gct = cost * parseFloat(GCT_RATE);
            $('#Invoice_GCT').val(gct);
            $('#Invoice_Total').val(cost + gct);
        }
    });

    $('#ServiceId').on('change', function (e) {
        var serviceId = $(this).val();
        if (serviceId) {
            $.ajax({
                cache: false,
                dataType: 'json',
                type: 'GET',
                url: '/api/service/' + serviceId,
                success: function (data) {
                    if (data) {
                        $('#Invoice_ServiceCost').val(data.Cost).trigger('change');
                        $('#Package_Weight').trigger("change");
                        $('#Invoice_DiscountType').trigger("change");
                    } else {
                        $('#Invoice_ServiceCost').val('');
                        $('#Invoice_GCT').val('');
                        $('#Invoice_Total').val('');
                    }
                },
                error: function () {
                    $('#Invoice_ServiceCost').val('');
                    $('#Invoice_GCT').val('');
                    $('#Invoice_Total').val('');
                }
            });
        }
    });
});