/* ====== MENU  ====== */$(document).ready(function() {var site = function() {	this.navLi = $('#menu li').children('ul').hide().end();	this.init();};site.prototype = { 	 	init : function() { 		this.setMenu(); 	}, 	 	setMenu : function() { 	 	$.each(this.navLi, function() { 		if ( $(this).children('li ul li ul')[0] ) { 			$(this) 				.append('<span />') 				.children('span') 					.addClass('menuChildren') 		} 	}); 	 		this.navLi.hover(function() {			$(this).find('> ul').stop(true, true).slideDown('medium', 'easeOutCubic');			$(this).children('a:first').addClass("hov"); 		}, function() { 			$(this).find('> ul').stop(true, true).hide(); 			$(this).children('a:first').removeClass("hov");					}); 		 	} };new site();});/* ====== BOX TOOGLE ===== */$("div.hide").click(function () {$(".box .block",this).toggle("slow");$(this).toggleClass("show").next().slideToggle("medium");});/* ====== ALERTS ====== */$('.alert').click(function() {	 	$(this).hide('normal');		  });/* ====== GALLERY ====== */$(function() {   $('ul.gallery li').hover(function(){      $('img',this).animate({"opacity": "0.6"},{queue:true,duration:150});    }, function() {      $('img',this).animate({"opacity": "1"},{queue:true,duration:150});   });});/* ======== FORMS ======*/$(function(){    $("input:checkbox, input:radio, input:file").uniform();});$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({allow_single_deselect:true});/* ======== Placeholder ======*/
$(function() {    $('input, textarea').placeholder;});