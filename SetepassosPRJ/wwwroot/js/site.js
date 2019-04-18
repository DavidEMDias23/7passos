// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Código para conforme se "avança" de slide no carousel aparecer texto e esse texto ser adequado a imagem que aparece
$('#text').html($('.active > .carousel-caption').html());
$('.carousel').on('slid.bs.carousel', function (e) {
  	$('#text').html($('.active > .carousel-caption').html());
	});