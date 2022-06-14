$(document).ready(function () {
    $(".D").click(function (e) {
        var butten = $(e.target);
        var v = { Ditid: parseInt(butten.attr('data-ditid')) }
        if (butten.text().toString().includes('+ Fave Ditty?')) {
            $.post({
                url: '/api/favedit',
                data: JSON.stringify(v),
                contentType: 'application/json',
            })
                .done(function (data) {
                    butten
                        .css("box-shadow", "0.5rem 0.5rem #ccc, -0.5rem -0.5rem #f93636")
                        .text('Added')
                    console.log(data);
                })
                .fail(function (jqxhr, settings, ex) {
                    console.log('failed, ' + ex + jqxhr.responseText);
                })
        }
        else {
            $.ajax({
                url: "/api/favedit",
                data: JSON.stringify(v),
                method: "DELETE",
                contentType: 'application/json',
            }).done(function () {
                butten
                    .css("box-shadow", "0.5rem 0.5rem #ccc, -0.5rem -0.5rem #f93636")
                    .text('Removed')
                //    alert('Request done!');
            }).fail(function (jqxhr, settings, ex) {
                alert('failed, ' + ex + jqxhr.responseText);
            })
        }
    })

    $(".A").click(function (e) {
        var butten = $(e.target);
        var v = { FeeId: butten.attr('data-Feeid') }
        $.post({
            url: '/api/followings',
            data: JSON.stringify(v),
            contentType: 'application/json',
            dataType: 'json'
        })
            .done(function () {
                butten
                    .css("box-shadow", "0.5rem 0.5rem #ccc, -0.5rem -0.5rem #f93636")
                    .text('followed')
                alert('Request done!');
            })
            .fail(function (jqxhr, settings, ex) {
                butten
                    .css("box-shadow", "0.5rem 0.5rem #f93636, -0.5rem -0.5rem #ccc")
                    .text('failed')
                alert('failed, ' + ex + jqxhr.responseText);
            })

    })
})