﻿jQuery(function ($) {
    $('.delete-interest').click(function (e) {
        var interestId = $(e.target).closest('tr').find('.interest-id').val();

        $.ajax({
            headers: {
                'Accept': 'application/json'
            },
            type: 'POST',
            url: 'Interest/Delete',
            data: { id: interestId },
            success: function (response) {
                $(e.target).closest('tr').remove();
                alert('Interest deleted successfuly');
            },
            fail: function (ex) {
                alert('something went wrong');
            }
        });
    });
});