// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
const funt = require('./Hello.js');
$('onload', function () {
    //$.getScript('/js/Hello.js', function () {
    //    hello();
    //});
    funt.hello();
})