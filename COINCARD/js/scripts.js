$('.img_outer .table_cell img').hover(function(){
	var img_src = $(this).attr('data-img');
	var src = $(this).attr('src');
	$(this).attr('src', img_src);
	$(this).attr('data-img', src);
}, function(){
	var img_src = $(this).attr('data-img');
	var src = $(this).attr('src');
	$(this).attr('src', img_src);
	$(this).attr('data-img', src);
});

$('.xans-layout-category > a').click(function(e){
	e.preventDefault();
	$(this).parent().find('ul').slideToggle()
})

$('.menu-button').click(function(){
	$('.main2cat').show();
	$('.overlay').show()
})
$('.overlay').click(function(){
	$('.main2cat').hide();
	$('.overlay').hide();
})
setTimeout(function(){
	if ($(window).width() > 768){
		var height1 = $("#neoher-top").outerHeight();
		var height2 = $(".footer-new").outerHeight();
		var height_all = $(window).height() - (height1 + height2)

		$(".home-banner").css("max-height",height_all);
	}

	if ($(window).width() < 768){

		$(".home-banner-mobile").click(function(){

			var href = $(this).attr("data-href");
			window.location.href = href

		});

		var height1 = $("#neo-mobile").outerHeight();
		var height_all = $(window).height();
		$(".home-banner-mobile").css("height",height_all);

		

	}

},500);