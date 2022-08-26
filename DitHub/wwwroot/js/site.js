var DitService = function () {
    var createFDit = function (v, d, f) {
        $.post({
            url: '/api/favedits',
            data: JSON.stringify(v),
            contentType: 'application/json',
        })
            .done(d)
            .fail(f)
    }

    var deleteFDit = function (v, d, f) {
        $.ajax({
            url: "/api/favedits",
            data: JSON.stringify(v),
            method: "DELETE",
            contentType: 'application/json',
        })
            .done(d)
            .fail(f)
    }
    return {
        create: createFDit,
        delete: deleteFDit
    }
}();
var DitController = function (ditService) {
    var button;
    var init = function (container) {
        $(container).on("click", ".D", DEvent);
        //    $(".listDA").on("click", ".D", DEvent);
    }
    var DEvent = function (e) {
        button = $(e.target);
        var v = { Ditid: parseInt(button.attr('data-ditid')) }
        if (button.text().toString().includes('+ Fave Ditty?')) {
            ditService.create(v, done, fail)
        }
        else {
            ditService.delete(v, done, fail)
        }
    }
    var done = function () {
        var text = (button.text() == "Added") ? "Removed" : "Added"
        button
            .css("box-shadow", "0.5rem 0.5rem #ccc, -0.5rem -0.5rem #f93636")
            .text(text)
    }
    var fail = function (jqxhr, settings, ex) {
        alert('failed, ' + ex + jqxhr.responseText);
    }

    return {
        val: init
    }
}(DitService);

var FollowService = function () {
    var createF = function (data, d, f) {
        $.post({
            url: '/api/followings',
            data: JSON.stringify(data),
            contentType: 'application/json',
        })
            .done(d)
            .fail(f)
    }

    var deleteF = function (data, d, f) {
        $.ajax({
            url: "/api/followings",
            data: JSON.stringify(data),
            method: "DELETE",
            contentType: 'application/json',
        })
            .done(d)
            .fail(f)
    }

    return {
        create: createF,
        delete: deleteF
    }
}();
var FollowController = function (followService) {
    var button;
    var init = function (container) {
        $(container).on("click", ".A", Aevent);
        //    $("listDA").on("click", ".A", Aevent)
    }
    var Aevent = function (e) {
        button = $(e.target);
        var v = { FeeId: button.attr('data-Feeid') }
        if (button.text().toString().includes("+ Fave Artist?")) {
            followService.create(v, done, fail)
        } else {
            followService.delete(v, done, fail)
        }

    }
    var done = function () {
        var text0 = button.text() == "+ Fave Artist?" ? "A Is + Fave." : "+ Fave Artist?";
        var text = button.text() == "+ Fave Artist?" ? "Followed" : "Unfollowed";
        button
            .css("box-shadow", "0.5rem 0.5rem #ccc, -0.5rem -0.5rem #f93636")
            .text(text)
        wait(text0);
    }

    var fail = function (jqxhr, settings, ex) {
        var text = button.text();
        button
            .css("box-shadow", "0.5rem 0.5rem #ccc, -0.5rem -0.5rem #f93636")
            .text('Failed');
        wait(text);
    }

    var wait = function (text) {
        setTimeout(function () {
            var id = button.attr("data-Feeid");
            var t = button.text();
            button
                .css("box-shadow", "0.5rem 0.5rem #f93636, -0.5rem -0.5rem #ccc")
                .text(text)
            if (t != "Failed") {
                $("[data-Feeid=" + id + "]").each(function () {
                    $(this).text(text);
                });
            }
        }, 2000);
    }
    return {
        val: init
    }
}(FollowService);
