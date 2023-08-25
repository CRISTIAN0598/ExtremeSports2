//copy menu for mobile
function copyMenu() {
    //Copy inside .dpt-cat to .departments
    // let dpCategory = document.querySelector('.dpt-cat');
    // let dptPlace=document.querySelector('.departments');
    // dptPlace.innerHTML=dpCategory.innerHTML;

    //Copy inside nav to nav
    let mainNav = document.querySelector('.header-nav nav');
    let navPlace = document.querySelector('.off-canvas nav');
    navPlace.innerHTML = mainNav.innerHTML;

    //Copy .header-top .wrapper to .thetop-nav
    let topNav = document.querySelector('.header-top .wrapper');
    let topPlace = document.querySelector('.off-canvas .thetop-nav');
    topPlace.innerHTML = topNav.innerHTML;
}
copyMenu();

//Show mobile menu
let menuButton = document.querySelector('.trigger'),
    closeButton = document.querySelector('.t-close'),
    addClass = document.querySelector('.site');

menuButton.addEventListener('click', function () {
    addClass.classList.toggle('showmenu')
})

closeButton.addEventListener('click', function () {
    addClass.classList.remove('showmenu')
})


//Show sub menu on mobile
let submenu = document.querySelectorAll('.has-child .icon-small');
submenu.forEach(menu => menu.addEventListener('click', toggle));

function toggle(e) {
    e.preventDefault();
    submenu.forEach((item) => item != this ? item.closest('.has-child').classList.remove('expand') : null);
    if (this.closest('.has-child').classList != 'expand');
    this.closest('.has-child').classList.toggle('expand');
}

//Slider
// const swiper = new Swiper('.swiper', {
//     loop: true,
//     pagination: {
//       el: '.swiper-pagination',
//     },
//   });

var swiper = new Swiper(".mySwiper", {
    effect: "coverflow",
    grabCursor: true,
    centeredSlides: true,
    slidesPerView: "auto",
    coverflowEffect: {
        rotate: 50,
        stretch: 0,
        depth: 100,
        modifier: 1,
        slideShadows: true,

    },
    pagination: {
        el: ".swiper-pagination",
    },
});

// var swiper2 = new Swiper(".mySwiper2", {
//   slidesPerView: 3,
//   spaceBetween: 30,
//   pagination: {
//     el: ".swiper-pagination",
//     clickable: true,
//   },
// });
var swiper2 = new Swiper(".mySwiper2", {
    loop: false,
    slidesPerView: 'auto',
    spaceBetween: 33,
    centeredSlides: true,
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },

});

// show search
let searchButton = document.querySelector('.t-search'),
    tClose = document.querySelector('.search-close'),
    showClass = document.querySelector('.site');
searchButton.addEventListener('click', function () {
    showClass.classList.toggle('showsearch')
})
tClose.addEventListener('click', function () {
    showClass.classList.remove('showsearch')
})

//Shoe dptmenu

// const dptButton= document.querySelector('.dpt-cat .dpt-trigger'),
// dptClass=document.querySelector('.site');
// dptButton.addEventListener('click',function()
// {
//   dptClass.classList.toggle('showdpt');
// })

//Product image slider
let productThumb = new Swiper('.small-image', {
    loop: true,
    spaceBetween: 10,
    slidesPerView: 3,
    freeMode: true,
    watchSlidesProgress: true,
    breakpoints: {
        481: {
            spaceBetween: 32,
        }
    }
});

let productBig = new Swiper('.big-image', {
    loop: true,
    autoHeight: true,
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev'
    },
    thumbs: {
        swiper: productThumb,
    }
})

// //SAtock products bar width percentage

// let stocks= document.querySelectorAll('.products .stock');
// for(let x = 0;x < stocks.length; x++){

//   let stock = stocks[x].dataset.stock,
//   available= stocks[x].querySelector('.qty-available').innerHTML,
//   sold= stocks[x].querySelector('.qty-sold').innerHTML,
//   percent= sold*100/stock;
//   stocks[x].querySelector('.available').style.width=percent + '%';
// }




//Show cart on click

const divtoShow = '.mini-cart';
const divPopup = document.querySelector(divtoShow);
const divTrigger = document.querySelector('.cart-trigger');

divTrigger.addEventListener('click', () => {
    setTimeout(() => {
        if (!divPopup.classList.contains('show')) {
            divPopup.classList.add('show');
        }
    }, 250)
})

//close by click outside
//document.addEventListener('click', (e) => {
//    const isClosest = e.target.closest(divtoShow);
//    if (!isClosest && divPopup.classList.contains('show')) {
//        divPopup.classList.remove('show')
//    }
//})

  //Show modal on load
  // window.onload= function(){
  // document.querySelector('.site').classList.toggle('showmodal')
  // }
   //document.querySelector('.modalclose').addEventListener('click', function(){
   //    document.querySelector('.site').classList.remove('showmodal')
   //})


//Picture Preview
const defaultFile ='https://localhost:7161/images/NoImage.jpg'
const file = document.getElementById('file');
const img = document.getElementById('img');
file.addEventListener('change', e => {
    if (e.target.files[0]) {
        let reader = new FileReader();
        reader.onload = function (e) {
            img.src = e.target.result;
        }
        reader.readAsDataURL(e.target.files[0])
    }
    else {
        img.src = defaultFile;
    }
})