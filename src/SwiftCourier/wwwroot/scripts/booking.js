$(document).ready(function () {
    var GCT_RATE = 0.165;

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

    $('#Invoice_ServiceCost').on("change", function (e) {
        var cost = parseFloat($(this).val());
        if (cost) {
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