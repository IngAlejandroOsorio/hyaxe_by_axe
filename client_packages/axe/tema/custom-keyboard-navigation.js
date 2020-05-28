/**
 * Javascript functions 
 * @author: Jaime Beltran
 * version: 1
 * date: 2018-11-23
 * requires: jQuery
 */



function initNavigate(){
	//console.log("call initNavigate");
	$('.selectable:visible').navigate('destroy');
	//console.log(current_sel);
	$('.selectable:visible').navigate({
		activeClass: 'selected',
		selectedDefault: $("#selectedDefault"),
		onSelect:function(){
			//$('.selectable').navigate('destroy');
			//$(this).find("a")[0].click();
		},
		onFocus:function(){
			//console.log(this);
		}
	});
}

function iniMenusActions(){
	/**
	 * Menu navigation with keyboard
	 */
	$(".activable").on("click",function(){
		$(".activable").removeClass('active');
		$(this).addClass('active');
	});

	//this requires jquery.navigate.js file or  jquery.navigate.min.js
	initNavigate();

	$('.collapse').on('hidden.bs.collapse', function () {
	  //console.log("collpase hide");
	  initNavigate();
	});

	$('.collapse').on('show.bs.collapse', function () {
	  //console.log("collpase show");
	  window.setTimeout( function(){
	  	initNavigate();
	  }, 500);
	});


	$('.modal').on('show.bs.modal', function (event) {
  		window.setTimeout( function(){
		  	initNavigate();
		  }, 500);
	});

	$('.modal').on('hide.bs.modal', function (event) {
		 initNavigate();
	});
}