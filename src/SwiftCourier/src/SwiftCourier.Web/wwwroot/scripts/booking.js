$(document).ready(function () {
    $('#Invoice_ServiceCost').attr('readonly', true);
    $('#Invoice_GCT').attr('readonly', true);
    $('#Invoice_Total').attr('readonly', true);

    $('#CustomerId, #ServiceId, #Package_Weight, #Invoice_DiscountType, #Invoice_DiscountValue').on('change', function (e) {
        $.ajax({
            cache: false,
            dataType: 'json',
            data: {
                'customerId': $('#CustomerId').val(),
                'serviceId': $('#ServiceId').val(),
                'weight': $('#Package_Weight').val(),
                'discountType': $('#Invoice_DiscountType').val(),
                'discountValue': $('#Invoice_DiscountValue').val(),
            },
            type: 'GET',
            url: '/api/get_cost_data',
            success: function (data) {
                if (data && data.success) {
                    console.log(data);
                    $('#Invoice_ServiceCost').val(data.service_cost);
                    $('#Invoice_GCT').val(data.gct);
                    $('#Invoice_Total').val(data.total);
                    $('#Invoice_DiscountAmount').val(data.discount_amount);
                }
            },
            error: function () {
            }
        });
    });

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

                    }
                },
                error: function () {
                }
            });
        }
    });
});