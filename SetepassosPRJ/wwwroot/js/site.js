// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$('.carousel').carousel({
  //interval: 200
//});

$('#text').html($('.active > .carousel-caption').html());
$('.carousel').on('slid.bs.carousel', function (e) {
  	$('#text').html($('.active > .carousel-caption').html());
	});