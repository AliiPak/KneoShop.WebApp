function FillPage(page) {
    $('#Page').val(page);
    $('#filter-form').submit();
}
function AddProductToBasket(productID) {
    var count = $('#product_count').val();

    $.get('/add-to-basket?productID=' + productID + '&count=' + count).then(res => {
        if (res.status === 'NotAuthenticated') {
            alert('ابتدا باید لاگین کنید');
        } else if (res.status === 'Added') {
            alert('محصول با موفقیت به سبد خرید شما افزوده شد');
        } else {
            alert('خطایی رخ داد');
        }
    });
}