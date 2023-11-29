jQuery(function ($) {

    $('#myModal').on('show.bs.modal', function (e) {
        if (!data) return e.preventDefault();
    });

    function deleteCity(e) {

        var cityId = $(e.target).closest('tr').find('.city-id').val();
        var name = $(e.target).closest('tr').find('.city-name').text();

        if (cityId === undefined) {
            $(e.target).closest('tr').remove();
            return;
        }

        if (confirm("Are you sure you want to delete " + name + "?")) {

            $.ajax({
                headers: {
                    'Accept': 'application/json'
                },
                type: 'POST',
                url: 'City/Delete',
                data: { cityId: cityId },
                success: function (response) {
                    $(e.target).closest('tr').remove();
                    alert(name + " was deleted");
                },
                fail: function (ex) {
                    alert('something went wrong');
                }
            });
        }
    }

    $('#add-county').on('click', function () {

        var name = $('#county-name-input').val();
        var shortName = $('#county-short-name-input').val();

        $.ajax({
            headers: {
                'Accept': 'application/json'
            },
            type: 'POST',
            url: 'County/Add',
            data: { name: name, shortname: shortName },
            success: function (response) {
                $('#modal-edit-county').modal('hide');
                alert('County added successfuly');
                location.reload();
            },
            fail: function (ex) {
                alert('something went wrong');
            }
        });
    });

    $('#new-county').on('click', function () {
        $('#county-name-input').val("");
        $('#county-short-name-input').val("");

        $('#save-county-changes').hide();
        $('#add-county').show();

        $('#modal-edit-county').modal();
    });

    $('#add-city').on('click', function () {
        var name = $('#city-name-input').val();
        var countyId = $('#selected-county-id').val();

        $.ajax({
            headers: {
                'Accept': 'application/json'
            },
            type: 'POST',
            url: 'City/Add',
            data: { name: name, countyId: countyId },
            success: function (response) {
                $('#modal-edit-city').modal('hide');
                alert('City modified successfuly');

                var source = document.getElementById("city-template").innerHTML;
                var template = Handlebars.compile(source);
                var html = template({ name: name });
                $('#cities-table').prepend(html);

            },
            fail: function (ex) {
                alert('something went wrong');
            }
        });
    });

    $('#new-city').on('click', function () {
        $('#city-name-input').val("");
        $('#save-city-changes').hide();
        $('#add-city').show();
        $('#modal-edit-city').modal();
    });

    $('#cities-table ').on('click', '.delete', function (e) {
        deleteCity(e);
    });

    $('#counties-table ').on('click', '.edit', function (e) {
        var countyName = $(e.target).closest('tr').find('.county-name').text();
        var countyShortName = $(e.target).closest('tr').find('.county-short-name').text();
        var countyId = $(e.target).closest('tr').find('.county-id').val();

        $('#county-id').val(countyId);
        $('#county-name-input').val(countyName);
        $('#county-short-name-input').val(countyShortName);
        $('#save-county-changes').show();
        $('#add-county').hide();

        $('#modal-edit-county').modal();
    });

    $('#cities-table ').on('click', '.edit', function (e) {
        var cityName = $(e.target).closest('tr').find('.city-name').text();
        var cityId = $(e.target).closest('tr').find('.city-id').val();

        $('#city-id').val(cityId);
        $('#city-name-input').val(cityName);
        $('#save-city-changes').show();
        $('#add-city').hide();
        $('#modal-edit-city').modal();
    });

    function editCounty(name, shortname, id) {
        $.ajax({
            headers: {
                'Accept': 'application/json'
            },
            type: 'POST',
            url: 'County/Edit',
            data: { id: id, name: name, shortname: shortname },
            success: function (response) {
                $('#modal-edit-county').modal('hide');
                alert('County modified successfuly');
                location.reload();
            },
            fail: function (ex) {
                alert('something went wrong');
            }
        });
    }

    $('#save-county-changes').click(function () {
        var name = $('#county-name-input').val();
        var shortName = $('#county-short-name-input').val();
        var id = $('#county-id').val();

        if (name === "" || shortName === "") {
            alert('Both fields are required');
        }
        if (shortName.length > 2) {
            alert('Short Name can not have more than 2 letters');
        }
        else {
            editCounty(name, shortName, id);
        }
    });

    function editCity(id, name) {
        $.ajax({
            headers: {
                'Accept': 'application/json'
            },
            type: 'POST',
            url: 'City/Edit',
            data: { id: id, name: name },
            success: function (response) {
                $('#modal-edit-city').modal('hide');
                alert('City modified successfuly');
                location.reload();
            },
            fail: function (ex) {
                alert('something went wrong');
            }
        });
    }

    $('#save-city-changes').click(function () {
        var name = $('#city-name-input').val();
        var id = $('#city-id').val();

        if (name === "") {
            alert('Field Name is required');
        }
        else {
            editCity(id, name);
        }
    });

    function deleteCounty(ev) {
        var countyId = $(ev.target).closest('tr').find('.county-id').val();

        var name = $(ev.target).closest('tr').find('.county-name').text();

        if (confirm("Are you sure you want to delete " + name + "?")) {
            $.ajax({
                headers: {
                    'Accept': 'application/json'
                },
                type: 'POST',
                url: 'County/Delete',
                data: { id: countyId },
                success: function (response) {
                    if (response.id === 0) {
                        $(ev.target).closest('tr').remove();
                    }

                    alert(response.message);
                },
                fail: function (ex) {
                    alert('something went wrong');
                }
            });
        }
    }

    $('#counties-table').on('click', '.delete', function (e) {
        deleteCounty(e);
    });

    $("[data-toggle=tooltip]").tooltip();

    $('#counties-table').on('click', 'tr', function (e) {

        var row = $(e.target).closest('tr');
        var countyId = $(row).find('input').val();
        var nume = $(row).find('.county-name').text();

        $('#counties-table .selected').each(function (index, elem) {
            $(elem).removeClass('selected');
            //elem.removeAttr('background-color');
        });

        $(row).attr('selected');

        $('#selected-county-id').val(countyId);

        var source = document.getElementById("city-template").innerHTML;
        var template = Handlebars.compile(source);

        $.ajax({
            headers: {
                'Accept': 'application/json'
            },
            type: 'GET',
            url: 'City/Index',
            data: { id: countyId },
            success: function (response) {

                var tableBody = $('#cities-table tbody');

                $('#cities-table tbody tr').each(function (index, elem) {
                    elem.remove();
                });

                for (var i = 0; i < response.length; i++) {
                    var html = template(response[i]);
                    tableBody.append(html);
                }
            },
            fail: function (ex) {
                alert('something went wrong');
            }
        });

    });
});