(()=>{var e=globalThis,t={},o={},i=e.parcelRequirece5f;null==i&&((i=function(e){if(e in t)return t[e].exports;if(e in o){var i=o[e];delete o[e];var n={id:e,exports:{}};return t[e]=n,i.call(n.exports,n,n.exports),n.exports}var r=Error("Cannot find module '"+e+"'");throw r.code="MODULE_NOT_FOUND",r}).register=function(e,t){o[e]=t},e.parcelRequirece5f=i),(0,i.register)("gymiC",function(e,t){function o(e,t){console.log(e,t),t?e.removeClass("d-none"):e.addClass("d-none")}Object.defineProperty(e.exports,"toggleObjectVisibility",{get:()=>o,set:void 0,enumerable:!0,configurable:!0})});var n=i("gymiC");let r=$(".login-container"),s=$(".register-container");function l(e){switch(e){case"signup":(0,n.toggleObjectVisibility)(r,!1),(0,n.toggleObjectVisibility)(s,!0);break;case"login":(0,n.toggleObjectVisibility)(r,!0),(0,n.toggleObjectVisibility)(s,!1)}}async function c(){let e=document.getElementById("userId").value,t=document.getElementById("username").value,o=document.getElementById("password").value;try{let i=await fetch("/api/Authorization/RegisterUser",{method:"POST",headers:{"Content-Type":"application/json"},body:JSON.stringify({id:e,username:t,password:o})});if(!i.ok){let e=await i.text();throw Error(e)}console.log("User registered successfully!"),l("login")}catch(e){console.error("Error:",e)}}async function a(){let e=document.getElementById("username2").value,t=document.getElementById("password2").value;try{let o=await fetch("/api/Authorization/LoginUser",{method:"POST",headers:{"Content-Type":"application/json"},body:JSON.stringify({username:e,password:t})});if(!o.ok){let e=await o.text();throw Error(e)}await o.json(),console.log("User loginned successfully!"),l("login")}catch(e){console.error("Error:",e)}}$(document).ready(function(){$(".js-show-sign-up-form").click(function(){l("signup")}),$(".js-show-log-in-form").click(function(){l("login")}),$(".js-register-user").click(function(){c()}),$(".js-login-user").click(function(){a()})})})();
//# sourceMappingURL=auth.js.map