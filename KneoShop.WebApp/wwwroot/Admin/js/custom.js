function FillPage(page) {
    $('#Page').val(page);
    $('#filter-form').submit();
}

function readURL(input, priviewImg) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(priviewImg).attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("[ImageInput]").change(function () {
    var x = $(this).attr("ImageInput");
    if (this.files && this.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("[ImageFile=" + x + "]").attr('src', e.target.result);
        };
        reader.readAsDataURL(this.files[0]);
    }
});

function RemoveProduct(productId) {

    Swal.fire({
        title: 'اخطار',
        text: "آیا از حذف محصول مورد نظر اطمینان دارید؟",
        icon: 'danger',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText:'خیر'
    }).then((result) => {
        if (result.isConfirmed) {

            $.get('/Admin/Product/RemoveProduct/' + productId).then(res => {
                if (res.status === 'Success') {
                    $('#product-detail-' + productId).fadeOut();

                    Swal.fire(
                        'حذف شد!',
                        '',
                        'success'
                    )
                } else if (res.status === 'NotFound') {

                }
            });
        }
    })
}
function RemoveInventory(inventoryID) {

    Swal.fire({
        title: 'اخطار',
        text: "آیا از حذف ویژگی محصول مورد نظر اطمینان دارید؟",
        icon: 'danger',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText:'خیر'
    }).then((result) => {
        if (result.isConfirmed) {

            $.get('/Admin/Inventory/RemoveInventory/' + inventoryID).then(res => {
                if (res.status === 'Success') {
                    $('#inventory-detail-' + inventoryID).fadeOut();

                    Swal.fire(
                        'حذف شد!',
                        '',
                        'success'
                    )
                } else if (res.status === 'NotFound') {

                }
            });
        }
    })
}
function RemoveOrders(orderID) {

    Swal.fire({
        title: 'اخطار',
        text: "آیا از حذف ویژگی محصول مورد نظر اطمینان دارید؟",
        icon: 'danger',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText:'خیر'
    }).then((result) => {
        if (result.isConfirmed) {

            $.get('/Admin/Orders/RemoveOrder/' + orderID).then(res => {
                if (res.status === 'Success') {
                    $('#order-detail-' + inventoryID).fadeOut();

                    Swal.fire(
                        'حذف شد!',
                        '',
                        'success'
                    )
                } else if (res.status === 'NotFound') {

                }
            });
        }
    })
}
function RemoveOrderDetails(orderDetailID) {

    Swal.fire({
        title: 'اخطار',
        text: "آیا از حذف اقلام سفارش مورد نظر اطمینان دارید؟",
        icon: 'danger',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText:'خیر'
    }).then((result) => {
        if (result.isConfirmed) {

            $.get('/Admin/Orders/RemoveOrderDetail/' + orderDetailID).then(res => {
                if (res.status === 'Success') {
                    $('#orderDe-detail-' + orderDetailID).fadeOut();

                    Swal.fire(
                        'حذف شد!',
                        '',
                        'success'
                    )
                } else if (res.status === 'NotFound') {

                }
            });
        }
    })
}
function RemoveProductGallery(galleryId) {

    Swal.fire({
        title: 'اخطار',
        text: "آیا از حذف تصویر مورد نظر اطمینان دارید؟",
        icon: 'danger',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'آره، بحذفش'
    }).then((result) => {
        if (result.isConfirmed) {

            $.get('/Admin/Product/RemoveProductGallery/' + galleryId).then(res => {
                if (res.status === 'Success') {
                    $('#product-gallery-' + galleryId).fadeOut();

                    Swal.fire(
                        'حذف شد!',
                        'میتونی is delete رو false کنی که برگرده',
                        'success'
                    )
                } else if (res.status === 'NotFound') {

                }
            });
        }
    })
}

function RemoveUser(userID) {

    Swal.fire({
        title: 'اخطار',
        text: "آیا از حذف کاربر مورد نظر اطمینان دارید؟",
        icon: 'danger',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر'
    }).then((result) => {
        if (result.isConfirmed) {

            $.get('/Admin/Account/RemoveAccount/' + userID).then(res => {
                if (res.status === 'Success') {
                    $('#user-detail-' + userID).fadeOut();

                    Swal.fire(
                        'حذف شد!',
                        '',
                        'success'
                    )
                } else if (res.status === 'NotFound') {

                }
            });
        }
    })
}