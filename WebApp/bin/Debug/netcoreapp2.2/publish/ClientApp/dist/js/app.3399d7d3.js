(function(e){function t(t){for(var r,i,c=t[0],u=t[1],s=t[2],l=0,p=[];l<c.length;l++)i=c[l],o[i]&&p.push(o[i][0]),o[i]=0;for(r in u)Object.prototype.hasOwnProperty.call(u,r)&&(e[r]=u[r]);f&&f(t);while(p.length)p.shift()();return a.push.apply(a,s||[]),n()}function n(){for(var e,t=0;t<a.length;t++){for(var n=a[t],r=!0,i=1;i<n.length;i++){var u=n[i];0!==o[u]&&(r=!1)}r&&(a.splice(t--,1),e=c(c.s=n[0]))}return e}var r={},o={app:0},a=[];function i(e){return c.p+"js/"+({counter:"counter","fetch-data":"fetch-data"}[e]||e)+"."+{counter:"58d025a1","fetch-data":"9675cf0e"}[e]+".js"}function c(t){if(r[t])return r[t].exports;var n=r[t]={i:t,l:!1,exports:{}};return e[t].call(n.exports,n,n.exports,c),n.l=!0,n.exports}c.e=function(e){var t=[],n=o[e];if(0!==n)if(n)t.push(n[2]);else{var r=new Promise(function(t,r){n=o[e]=[t,r]});t.push(n[2]=r);var a,u=document.createElement("script");u.charset="utf-8",u.timeout=120,c.nc&&u.setAttribute("nonce",c.nc),u.src=i(e),a=function(t){u.onerror=u.onload=null,clearTimeout(s);var n=o[e];if(0!==n){if(n){var r=t&&("load"===t.type?"missing":t.type),a=t&&t.target&&t.target.src,i=new Error("Loading chunk "+e+" failed.\n("+r+": "+a+")");i.type=r,i.request=a,n[1](i)}o[e]=void 0}};var s=setTimeout(function(){a({type:"timeout",target:u})},12e4);u.onerror=u.onload=a,document.head.appendChild(u)}return Promise.all(t)},c.m=e,c.c=r,c.d=function(e,t,n){c.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:n})},c.r=function(e){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},c.t=function(e,t){if(1&t&&(e=c(e)),8&t)return e;if(4&t&&"object"===typeof e&&e&&e.__esModule)return e;var n=Object.create(null);if(c.r(n),Object.defineProperty(n,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var r in e)c.d(n,r,function(t){return e[t]}.bind(null,r));return n},c.n=function(e){var t=e&&e.__esModule?function(){return e["default"]}:function(){return e};return c.d(t,"a",t),t},c.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},c.p="/",c.oe=function(e){throw console.error(e),e};var u=window["webpackJsonp"]=window["webpackJsonp"]||[],s=u.push.bind(u);u.push=t,u=u.slice();for(var l=0;l<u.length;l++)t(u[l]);var f=s;a.push([0,"chunk-vendors"]),n()})({0:function(e,t,n){e.exports=n("cd49")},"92a1":function(e,t,n){"use strict";var r=n("eaf7"),o=n.n(r);o.a},cd49:function(e,t,n){"use strict";n.r(t);n("744f"),n("6c7b"),n("7514"),n("20d6"),n("1c4c"),n("6762"),n("cadf"),n("e804"),n("55dd"),n("d04f"),n("c8ce"),n("217b"),n("7f7f"),n("f400"),n("7f25"),n("536b"),n("d9ab"),n("f9ab"),n("32d7"),n("25c9"),n("9f3c"),n("042e"),n("c7c6"),n("f4ff"),n("049f"),n("7872"),n("a69f"),n("0b21"),n("6c1a"),n("c7c62"),n("84b4"),n("c5f6"),n("2e37"),n("fca0"),n("7cdf"),n("ee1d"),n("b1b1"),n("87f3"),n("9278"),n("5df2"),n("04ff"),n("f751"),n("4504"),n("fee7"),n("ffc1"),n("0d6d"),n("9986"),n("8e6e"),n("25db"),n("e4f7"),n("b9a1"),n("64d5"),n("9aea"),n("db97"),n("66c8"),n("57f0"),n("165b"),n("456d"),n("cf6a"),n("fd24"),n("8615"),n("551c"),n("097d"),n("df1b"),n("2397"),n("88ca"),n("ba16"),n("d185"),n("ebde"),n("2d34"),n("f6b3"),n("2251"),n("c698"),n("a19f"),n("9253"),n("9275"),n("3b2b"),n("3846"),n("4917"),n("a481"),n("28a5"),n("386d"),n("6b54"),n("4f7f"),n("8a81"),n("ac4d"),n("8449"),n("9c86"),n("fa83"),n("48c0"),n("a032"),n("aef6"),n("d263"),n("6c37"),n("9ec8"),n("5695"),n("2fdb"),n("d0b0"),n("5df3"),n("b54a"),n("f576"),n("ed50"),n("788d"),n("14b9"),n("f386"),n("f559"),n("1448"),n("673e"),n("242a"),n("c66f"),n("b05c"),n("34ef"),n("6aa2"),n("15ac"),n("af56"),n("b6e4"),n("9c29"),n("63d9"),n("4dda"),n("10ad"),n("c02b"),n("4795"),n("130f"),n("ac6a"),n("96cf");var r=n("2b0e"),o=n("bc3a"),a=n.n(o),i={},c=a.a.create(i);c.interceptors.request.use(function(e){return e},function(e){return Promise.reject(e)}),c.interceptors.response.use(function(e){return e},function(e){return Promise.reject(e)}),Plugin.install=function(e,t){e.axios=c,window.axios=c,Object.defineProperties(e.prototype,{axios:{get:function(){return c}},$axios:{get:function(){return c}}})},r["default"].use(Plugin);Plugin;var u=n("d437"),s=n.n(u),l=n("d421"),f=n.n(l),p=n("535c"),d=n.n(p),v=n("d0ba"),b=n.n(v),m=n("e1f0"),h=n.n(m),y=n("5d92"),g=n.n(y),_=n("6a6f"),w=n.n(_),j=n("d553"),k=n.n(j),P=n("12d0"),O=n.n(P),S=n("46db"),V=n.n(S),C=n("5933"),x=n.n(C),T=n("2330"),E=n.n(T);n("da64");r["default"].use(s.a,{components:{VApp:f.a,VNavigationDrawer:d.a,VFooter:b.a,VList:h.a,VBtn:g.a,VIcon:w.a,VGrid:k.a,VToolbar:O.a,VDataTable:V.a,VProgressLinear:x.a,transitions:E.a},theme:{primary:"#1976D2",secondary:"#424242",accent:"#82B1FF",error:"#FF5252",info:"#2196F3",success:"#4CAF50",warning:"#FFC107"}});var A=function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("v-app",[n("v-navigation-drawer",{attrs:{persistent:"","mini-variant":e.miniVariant,clipped:e.clipped,"enable-resize-watcher":"",fixed:"",app:""},model:{value:e.drawer,callback:function(t){e.drawer=t},expression:"drawer"}},[n("v-list",e._l(e.items,function(t,r){return n("v-list-tile",{key:r,attrs:{value:"true",to:t.link}},[n("v-list-tile-action",[n("v-icon",{domProps:{innerHTML:e._s(t.icon)}})],1),n("v-list-tile-content",[n("v-list-tile-title",{domProps:{textContent:e._s(t.title)}})],1)],1)}),1)],1),n("v-toolbar",{attrs:{app:"","clipped-left":e.clipped}},[n("v-toolbar-side-icon",{on:{click:function(t){t.stopPropagation(),e.drawer=!e.drawer}}}),n("v-btn",{attrs:{icon:""},on:{click:function(t){t.stopPropagation(),e.miniVariant=!e.miniVariant}}},[n("v-icon",{domProps:{innerHTML:e._s(e.miniVariant?"chevron_right":"chevron_left")}})],1),n("v-btn",{attrs:{icon:""},on:{click:function(t){t.stopPropagation(),e.clipped=!e.clipped}}},[n("v-icon",[e._v("web")])],1),n("v-toolbar-title",{domProps:{textContent:e._s(e.title)}}),n("v-spacer")],1),n("v-content",[n("router-view")],1),n("v-footer",{attrs:{app:""}},[n("span",[e._v(" Software Ateliers © 2019")])])],1)},F=[],W=n("9ab4"),H=function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("div",[n("blockquote",[e._v("\n    “"+e._s(e.quote)+"”\n    "),n("footer",[n("small",[n("em",[e._v("—"+e._s(e.author))])])])])])},I=[],q={name:"HelloWorld",props:{quote:String,author:String}},L=q,N=(n("92a1"),n("2877")),D=Object(N["a"])(L,H,I,!1,null,"69cc2703",null),M=D.exports,R=n("60a3");function $(e){return $="function"===typeof Symbol&&"symbol"===typeof Symbol.iterator?function(e){return typeof e}:function(e){return e&&"function"===typeof Symbol&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e},$(e)}function B(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function J(e,t){return!t||"object"!==$(t)&&"function"!==typeof t?z(e):t}function z(e){if(void 0===e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return e}function G(e){return G=Object.setPrototypeOf?Object.getPrototypeOf:function(e){return e.__proto__||Object.getPrototypeOf(e)},G(e)}function X(e,t){if("function"!==typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function");e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,writable:!0,configurable:!0}}),t&&Y(e,t)}function Y(e,t){return Y=Object.setPrototypeOf||function(e,t){return e.__proto__=t,e},Y(e,t)}var K=function(e){function t(){var e;return B(this,t),e=J(this,G(t).apply(this,arguments)),e.clipped=!0,e.drawer=!0,e.miniVariant=!1,e.right=!0,e.title="ASP.NET Core Vue Starter",e.items=[{title:"Home",icon:"home",link:"/"},{title:"Counter",icon:"touch_app",link:"/counter"},{title:"Fetch data",icon:"get_app",link:"/fetch-data"},{title:"Reality check",icon:"done_outline",link:"/reality"},{title:"DeadlineIsNear",icon:"timer",link:"/time"},{title:"Access",icon:"access_timer",link:"/access"},{title:"Shortee",icon:"warning",link:"/warning"}],e}return X(t,e),t}(R["b"]);K=W["a"]([Object(R["a"])({components:{HelloWorld:M}})],K);var Q=K,U=Q,Z=Object(N["a"])(U,A,F,!1,null,null,null),ee=Z.exports,te=n("8c4f"),ne=function(){var e=this,t=e.$createElement,r=e._self._c||t;return r("v-container",{attrs:{fluid:""}},[r("v-layout",{attrs:{column:"","align-center":""}},[r("img",{staticClass:"mb-5",attrs:{src:n("cf05"),alt:"Vuetify.js"}}),r("HelloWorld",{attrs:{quote:"Welcome to Your ASP.NET Core + Vue.js + TypeScript App",author:"brought to you by Software Ateliers"}})],1),r("v-slide-y-transition",{attrs:{mode:"out-in"}},[r("v-layout",{attrs:{column:""}},[r("h1",{staticClass:"headline"},[e._v("Hello, world!")]),r("p",[e._v("Welcome to your new single-page application, built with: ")]),r("ul",[r("li",[r("a",{attrs:{href:"https://get.asp.net",target:"_blank"}},[e._v(" ASP.NET Core")]),e._v(" and\n          "),r("a",{attrs:{href:"https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx",target:"_blank"}},[e._v("C#")]),e._v(" for cross-platform server-side API code")]),r("li",[r("a",{attrs:{href:"https://vuejs.org",target:"_blank"}},[e._v(" Vue.js")]),e._v(" for client-side code")]),r("li",[r("a",{attrs:{href:"https://cli.vuejs.org",target:"_blank"}},[e._v(" Vue CLI")]),e._v(" for building, bundling and adding or removing vue plugins")]),r("li",[r("a",{attrs:{href:"https://webpack.js.org",target:"_blank"}},[e._v("Webpack")]),e._v(" internally used by Vue CLI")]),r("li",[r("a",{attrs:{href:"https://vuetifyjs.com",target:"_blank"}},[e._v("Vuetify")]),e._v(" for layout and styling")])]),r("h3",{staticClass:"headline mt-3"},[e._v("To help you get started, we've also set up:")]),r("ul",[r("li",[r("strong",[e._v("Client-side navigation")]),e._v(". For example, click "),r("em",[e._v("Counter")]),e._v(" then "),r("em",[e._v("Back")]),e._v(" to return here.")]),r("li",[r("strong",[e._v("Development server integration")]),e._v(". In development mode, the development server from\n          "),r("code",[e._v("vue-cli-service")]),e._v(" runs in the background automatically, so your client-side resources are dynamically built on \n          demand and the page refreshes when you modify any file.")]),r("li",[r("strong",[e._v("Efficient production builds")]),e._v(". In production mode, development-time features are disabled, and the\n          "),r("code",[e._v("webpack")]),e._v(" build tool produces minified static CSS and JavaScript files.")])]),r("h3",{staticClass:"headline mt-3"},[e._v("Integrated Vue plugins:")]),r("ul",[r("li",[r("a",{attrs:{href:"https://vuejs.org",target:"_blank"}},[e._v(" Vue.js")]),e._v(" - reactive, component-oriented view layer for modern web interfaces.")]),r("li",[r("a",{attrs:{href:"https://vuetifyjs.com",target:"_blank"}},[e._v("Vuetify")]),e._v(" - a reusable semantic component framework for Vue.js.")]),r("li",[r("a",{attrs:{href:"https://github.com/vuejs/vue-class-component#readme",target:"_blank"}},[e._v("Class Component")]),e._v(" - ES201X/Typescript class decorator for Vue components.")]),r("li",[r("a",{attrs:{href:"https://github.com/kaorun343/vue-property-decorator#readme",target:"_blank"}},[e._v("Property Decorator")]),e._v(" - property decorators for Vue components")]),r("li",[r("a",{attrs:{href:"https://github.com/vuejs/vue-router#readme",target:"_blank"}},[e._v("Router")]),e._v(" - official router for Vue.js 2")]),r("li",[r("a",{attrs:{href:"https://github.com/vuejs/vuex#readme",target:"_blank"}},[e._v("Vuex")]),e._v(" - state management for Vue.js")]),r("li",[r("a",{attrs:{href:"https://github.com/yyx990803/register-service-worker#readme",target:"_blank"}},[e._v("Register Service Worker")]),e._v(" - script for registering service \n          worker with hooks for common events to simplify PWA development.")])])])],1)],1)},re=[];function oe(e){return oe="function"===typeof Symbol&&"symbol"===typeof Symbol.iterator?function(e){return typeof e}:function(e){return e&&"function"===typeof Symbol&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e},oe(e)}function ae(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function ie(e,t){return!t||"object"!==oe(t)&&"function"!==typeof t?ce(e):t}function ce(e){if(void 0===e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return e}function ue(e){return ue=Object.setPrototypeOf?Object.getPrototypeOf:function(e){return e.__proto__||Object.getPrototypeOf(e)},ue(e)}function se(e,t){if("function"!==typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function");e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,writable:!0,configurable:!0}}),t&&le(e,t)}function le(e,t){return le=Object.setPrototypeOf||function(e,t){return e.__proto__=t,e},le(e,t)}var fe=function(e){function t(){return ae(this,t),ie(this,ue(t).apply(this,arguments))}return se(t,e),t}(R["b"]);fe=W["a"]([Object(R["a"])({components:{HelloWorld:M}})],fe);var pe=fe,de=pe,ve=Object(N["a"])(de,ne,re,!1,null,null,null),be=ve.exports;r["default"].use(te["a"]);var me=new te["a"]({mode:"history",base:"/",routes:[{path:"/",name:"home",component:be},{path:"/counter",name:"counter",component:function(){return n.e("counter").then(n.bind(null,"80d5"))}},{path:"/fetch-data",name:"fetch-data",component:function(){return n.e("fetch-data").then(n.bind(null,"c1ce"))}}]}),he=n("2f62"),ye={currentCount:function(e){return e.counter}},ge={increment:function(e){var t=e.commit;t("incrementCounter")},reset:function(e){var t=e.commit;t("resetCounter")}},_e={incrementCounter:function(e){e.counter++},resetCounter:function(e){e.counter=0}},we={counter:0},je=!0,ke={namespaced:je,state:we,getters:ye,actions:ge,mutations:_e};r["default"].use(he["a"]);var Pe={state:{version:"1.0.0"},modules:{counter:ke}},Oe=new he["a"].Store(Pe),Se=n("9483");Object(Se["a"])("".concat("/","service-worker.js"),{ready:function(){console.log("App is being served from cache by a service worker.\nFor more details, visit https://goo.gl/AFskqB")},cached:function(){console.log("Content has been cached for offline use.")},updated:function(){console.log("New content is available; please refresh.")},offline:function(){console.log("No internet connection found. App is running in offline mode.")},error:function(e){console.error("Error during service worker registration:",e)}}),r["default"].config.productionTip=!1,new r["default"]({router:me,store:Oe,render:function(e){return e(ee)}}).$mount("#app")},cf05:function(e,t,n){e.exports=n.p+"img/logo.978a7dfc.png"},eaf7:function(e,t,n){}});
//# sourceMappingURL=app.3399d7d3.js.map