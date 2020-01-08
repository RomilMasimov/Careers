
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

SetRatingStar();
SetRatingStar1();

//create.cshtml

$('#categories').change(async function () {
    let categoryId = $('#categories option:selected').first().val();
    if (categoryId == 0) {
        $('#subCategories').children().slice(1).remove();
    } else {
        let subCategories = await (await fetch(`SubCategoryOptions?categoryId=${categoryId}`)).text();
        $('#subCategories').html(subCategories);
    }
});
$('#subCategories').change(async function () {
    let subCategoryId = $('#subCategories option:selected').first().val();
    if (subCategoryId == 0) {
        $('#services').children().slice(1).remove();
        $('#quetions').html('');
    } else {
        let services = await (await fetch(`ServicesOptions?subCategoryId=${subCategoryId}`)).text();
        $('#services').html(services);
        let questions = await (await fetch(`Questions?subCategoryId=${subCategoryId}`)).text();
        $('#quetions').html(questions);
    }
});
$('#services').change(async function () {
    let serviceId = $('#services option:selected').first().val();
    let subCategoryId = $('#subCategories option:selected').first().val();
    if (serviceId == 0) {
        let questions = await (await fetch(`Questions?subCategoryId=${subCategoryId}`)).text();
        $('#quetions').html(questions);
    } else {
        let questions = await (await fetch(`Questions?subCategoryId=${subCategoryId}&serviceId=${serviceId}`))
            .text();
        $('#quetions').html(questions);
    }
});

//end