function addToBasket(productId, productType) {
	var url = '/api/basketservice';
	$.post(url, { ProductId: productId, ProductTypeName: productType, Quantity: 1 }, function (data) {
			//alert(data);
			showMessage("Product added to the basket", true);
		})
		.fail(function() {
			alert("error");
		});
}

function showMessage(message, autoRefresh) {
	$('div#messages').html(message);

	if (autoRefresh) {
		window.setTimeout(clearMessages, 3000);
	}
}

function clearMessages() {
	$('div#messages').html('');
}