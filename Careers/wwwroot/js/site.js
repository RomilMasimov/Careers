
$('#next-button').on('click', function () {
    let res = parseInt($('#Filter_Page').val()) + 1;
    $('#Filter_Page').val(res);
    $("#main-form").submit();
});


$('.specialist-description').each(function (index) {
    this.innerText = this.innerText.substr(0, 250) + '...';
});


$('#category').change(function () {
    $("#main-form").submit();
});

$(":checkbox").change(function () {
    if (typeof $(this).data('single') !== 'undefined') {
        console.log('works');
        $('[data-single="1"]').attr('checked', false);
        this.checked = true;
    }
    $("#main-form").submit();
});


var $star_rating = $('.star-rating .yes');

var SetRatingStar = function () {
    return $star_rating.each(function () {
        if (parseInt($star_rating.siblings('input.rating-value').val()) >= parseInt($(this).data('rating'))) {
            return $(this).removeClass('fa-star-o').addClass('fa-star');
        } else {
            return $(this).removeClass('fa-star').addClass('fa-star-o');
        }
    });
};


$star_rating.on('click', function () {
    if ($(this).hasClass('not') == false) {
        $star_rating.siblings('input.rating-value').val($(this).data('rating'));
        SetRatingStar();
        $("#main-form").submit();

    }
});


var $star_rating1 = $('.star-rating .not');

var SetRatingStar1 = function () {
    return $star_rating1.each(function () {
        if (parseInt($star_rating1.siblings('input.rating-value').val()) >= parseInt($(this).data('rating'))) {
            return $(this).removeClass('fa-star-o').addClass('fa-star');
        } else {
            return $(this).removeClass('fa-star').addClass('fa-star-o');
        }
    });
};

function textAreaAdjust(o) {
    o.style.height = "1px";
    o.style.height = (25 + o.scrollHeight) + "px";
}


SetRatingStar();
SetRatingStar1();


