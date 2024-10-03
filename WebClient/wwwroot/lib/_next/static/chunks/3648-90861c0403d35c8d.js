"use strict";(self.webpackChunk_N_E=self.webpackChunk_N_E||[]).push([[3648],{3742:function(e,t,i){i.d(t,{F:function(){return u}});var n=i(47568),l=i(51438),a=i(52951),o=i(88029),r=i(60460),s=i(97582),c=i(6154),d=i(32191),u=(i(96486),function(e){(0,o.Z)(i,e);var t=(0,r.Z)(i);function i(){return(0,l.Z)(this,i),t.call(this,"subscriptions")}return(0,a.Z)(i,[{key:"createSubscriptions",value:function(e){var t=this;return(0,n.Z)((function(){var i;return(0,s.__generator)(this,(function(n){switch(n.label){case 0:return i=t.getConfig(),[4,c.Z.post("".concat(t.baseUrl),e,i)];case 1:return[2,n.sent()]}}))}))()}},{key:"updateSubscriptions",value:function(e){var t=this;return(0,n.Z)((function(){var i;return(0,s.__generator)(this,(function(n){switch(n.label){case 0:return i=t.getConfig(),[4,c.Z.put("".concat(t.baseUrl),e,i)];case 1:return[2,n.sent()]}}))}))()}},{key:"getSubscriptionsPackage",value:function(e){var t=this;return(0,n.Z)((function(){var i;return(0,s.__generator)(this,(function(n){switch(n.label){case 0:return i=t.getConfig(),[4,c.Z.post("".concat(t.baseUrl,"/list"),e,i)];case 1:return[2,n.sent()]}}))}))()}},{key:"getRecruiter",value:function(e){var t=this;return(0,n.Z)((function(){var i;return(0,s.__generator)(this,(function(n){switch(n.label){case 0:return i=t.getConfig(),[4,c.Z.post("".concat(t.baseUrl,"/recruiter"),e,i)];case 1:return[2,n.sent()]}}))}))()}},{key:"updateRecruiterSubscription",value:function(e){var t=this;return(0,n.Z)((function(){var i;return(0,s.__generator)(this,(function(n){switch(n.label){case 0:return i=t.getConfig(),[4,c.Z.put("".concat(t.baseUrl,"/recruiter"),e,i)];case 1:return[2,n.sent()]}}))}))()}},{key:"getMySubscriptionsPackage",value:function(){var e=this;return(0,n.Z)((function(){var t;return(0,s.__generator)(this,(function(i){switch(i.label){case 0:return t=e.getConfig(),[4,c.Z.get("".concat(e.baseUrl,"/my-subscription"),t)];case 1:return[2,i.sent()]}}))}))()}},{key:"registerSubscriptionsPackage",value:function(e){var t=this;return(0,n.Z)((function(){var i;return(0,s.__generator)(this,(function(n){switch(n.label){case 0:return i=t.getConfig(),[4,c.Z.post("".concat(t.baseUrl,"/register"),e,i)];case 1:return[2,n.sent()]}}))}))()}},{key:"registerApplyVoucherSubscriptionsPackage",value:function(e){var t=this;return(0,n.Z)((function(){var i;return(0,s.__generator)(this,(function(n){switch(n.label){case 0:return i=t.getConfig(),[4,c.Z.post("".concat(t.baseUrl,"/register/apply-voucher"),e,i)];case 1:return[2,n.sent()]}}))}))()}},{key:"getAllCredits",value:function(e){var t=this;return(0,n.Z)((function(){var i;return(0,s.__generator)(this,(function(n){switch(n.label){case 0:return i=t.getConfig(),[4,c.Z.post("".concat(t.baseUrl,"/recruiter/credit"),e,i)];case 1:return[2,n.sent()]}}))}))()}},{key:"getAllRecruiter",value:function(){var e=this;return(0,n.Z)((function(){var t;return(0,s.__generator)(this,(function(i){switch(i.label){case 0:return t=e.getConfig(),[4,c.Z.post("".concat(e.baseUrl,"/recruiter/all"),t)];case 1:return[2,i.sent()]}}))}))()}},{key:"activateSubscription",value:function(e){var t=this;return(0,n.Z)((function(){var i;return(0,s.__generator)(this,(function(n){switch(n.label){case 0:return i=t.getConfig(),[4,c.Z.post("".concat(t.baseUrl,"/activation"),e,i)];case 1:return[2,n.sent()]}}))}))()}},{key:"getConfig",value:function(){var e=localStorage.getItem("accessToken");return{headers:{"Content-Type":"application/json;charset=UTF-8","Access-Control-Allow-Origin":"*",Authorization:"Bearer ".concat(e)}}}}]),i}(d.b))},13648:function(e,t,i){i.r(t);var n=i(47568),l=i(26042),a=i(97582),o=i(85893),r=i(67294),s=i(31001),c=i(75982),d=i(11752),u=i.n(d),v=i(18185),m=i(4644),h=i(42441),p=i(57074),g=i(99403),x=i(4710),b=i(25852),f=i(88988),j=i(411),N=i(33270),k=i(74420),y=i(85518),C=i(30381),w=i.n(C),P=i(96486),S=i.n(P),_=i(40424),Z=i(9473),D=i(53368),L=i(3742),M=i(94387);t.default=function(e){var t=e.username,i=e.visible,d=(e.setVisibleSidebar,(0,Z.I0)()),C=(0,Z.v9)((function(e){var t;return null===e||void 0===e||null===(t=e.auth)||void 0===t?void 0:t.isLoggedIn})),P=(0,r.useState)(),T=(P[0],P[1]),V=(0,r.useState)(!1),A=V[0],F=V[1],U=(0,r.useState)(null),R=U[0],Q=U[1],I=(0,r.useState)(!1),Y=I[0],E=I[1],O=(0,r.useState)(!1),z=O[0],G=O[1],q=(0,r.useState)(!1),B=q[0],W=q[1],X=(0,r.useState)([]),K=(X[0],X[1]),H=(0,r.useState)(0),J=H[0],$=(H[1],u()().publicRuntimeConfig.contextPath),ee=(0,r.useRef)(null),te=(0,r.useRef)(null),ie=(0,r.useState)(!1),ne=ie[0],le=ie[1],ae=(0,r.useState)(!1),oe=ae[0],re=ae[1],se=new L.F,ce=new c.U,de=(0,r.useState)(!1),ue=de[0],ve=de[1],me=localStorage,he=(0,r.useState)([]),pe=he[0],ge=he[1],xe=(0,r.useState)(!1),be=xe[0],fe=xe[1],je=(0,r.useState)(!1),Ne=je[0],ke=je[1];(0,r.useEffect)((function(){ve(C)}),[C]),(0,r.useEffect)((0,n.Z)((function(){var e,n,l,o,r,s,c;return(0,a.__generator)(this,(function(a){switch(a.label){case 0:if(!i)return[2];E(!0),a.label=1;case 1:return a.trys.push([1,4,,5]),t?[4,ce.getDetailTiktokProfileNotLogin(t)]:[3,3];case 2:"success"==(e=a.sent()).data.code&&(ge(null===e||void 0===e||null===(n=e.data)||void 0===n?void 0:n.data),Q(null===e||void 0===e||null===(l=e.data)||void 0===l?void 0:l.data),(null===e||void 0===e||null===(o=e.data)||void 0===o||null===(r=o.data)||void 0===r?void 0:r.tiktokProfileVideos)&&K(null===e||void 0===e||null===(s=e.data)||void 0===s||null===(c=s.data)||void 0===c?void 0:c.tiktokProfileVideos)),a.label=3;case 3:return[3,5];case 4:return a.sent(),[3,5];case 5:return E(!1),[2]}}))})),[i,t,J,B,be]),(0,r.useEffect)((function(){C&&se.getMySubscriptionsPackage().then((function(e){var t,i;"success"===(null===e||void 0===e||null===(t=e.data)||void 0===t?void 0:t.code)&&T(null===e||void 0===e||null===(i=e.data)||void 0===i?void 0:i.data)})).catch((function(e){}))}),[C]);!function(){var e=(0,n.Z)((function(e,t){return(0,a.__generator)(this,(function(i){switch(i.label){case 0:return t?le(!ne):re(!oe),[4,navigator.clipboard.writeText(e)];case 1:return i.sent(),te.current.show({severity:"success",summary:"Th\xe0nh c\xf4ng",detail:"Copy to clipboard",life:3e3}),[2]}}))}))}();var ye=function(e){return"https://www.tiktok.com/@".concat(e)},Ce=function(e){var t,i=e.dataChartDoughnut;return(0,o.jsx)("div",{className:"border-round-xl col col-12 md:col-6",children:(0,o.jsxs)("div",{className:"items-interaction p-3 ".concat(null===i||void 0===i||null===(t=i.data)||void 0===t?void 0:t.style),children:[(0,o.jsxs)("div",{className:"header-interaction text-center",children:[(0,o.jsx)("i",{className:"pi ".concat(null===i||void 0===i?void 0:i.icon," mr-2")}),null===i||void 0===i?void 0:i.title]}),(0,o.jsx)("div",{className:"info-interaction",children:(0,o.jsxs)("div",{className:"grid align-items-center",children:[(0,o.jsxs)("div",{className:"col col-8 py-0 info-interaction-left font-bold text-center",children:[parseFloat(null===i||void 0===i?void 0:i.valuePercent),"%",(0,o.jsx)("br",{}),(0,o.jsx)("span",{className:"text-".concat(null===i||void 0===i?void 0:i.style,"-500"),children:null===i||void 0===i?void 0:i.titlePercentType})]}),(0,o.jsx)("div",{className:"col col-4 py-0 info-interaction-right",children:(0,o.jsx)(j.default,{data:{data:null===i||void 0===i?void 0:i.value,style:null===i||void 0===i?void 0:i.style}})})]})}),(0,o.jsxs)("span",{className:"sub-title text-center block",children:[null===i||void 0===i?void 0:i.totalPercent," l\xe0 s\u1ed1 li\u1ec7u trung b\xecnh"]})]})})},we=function(e){var t=e.dataChartDoughnutMulti;return(0,o.jsx)("div",{className:"items-followers col col-12 md:col-6",children:(0,o.jsx)(N.default,{dataKey:null===t||void 0===t?void 0:t.element,data:t},"".concat(null===t||void 0===t?void 0:t.element,"-TempalteItemsFollowers"))},null===t||void 0===t?void 0:t.element)},Pe=function(){return ue||"REC"==(null===me||void 0===me?void 0:me.role)?(0,o.jsx)(o.Fragment,{}):(0,o.jsx)("div",{className:"box-hidden-data",children:(0,o.jsxs)("div",{className:"box p-4 border-round",children:[(0,o.jsx)("h3",{children:'Vui l\xf2ng \u0111\u0103ng nh\u1eadp t\xe0i kho\u1ea3n "Nh\xe0 Tuy\u1ec3n D\u1ee5ng"'}),(0,o.jsx)("span",{children:"\u0110\u0103ng nh\u1eadp t\xe0i kho\u1ea3n Nh\xe0 tuy\u1ec3n d\u1ee5ng \u0111\u1ec3 xem mi\u1ec5n ph\xed d\u1eef li\u1ec7u chi ti\u1ebft"}),(0,o.jsx)("div",{className:"mt-3",children:(0,o.jsx)(g.z,{onClick:function(){return d((0,D.PM)())},label:"\u0110\u0103ng nh\u1eadp ngay"})})]})})};return(0,o.jsxs)(o.Fragment,{children:[(0,o.jsx)(M.default,{setChanging:fe,changing:be,dataTiktoker:pe,visibleRight:Ne,setVisibleRight:ke}),(0,o.jsx)(k.F,{ref:ee,position:"top-left"}),(0,o.jsx)(k.F,{ref:te,position:"top-left"}),(0,o.jsx)(_.Q,{style:{maxWidth:"320px"},visible:A,onHide:function(){return F(!1)},header:"B\u1ea1n c\u1ea7n \u0111\u0103ng k\xfd g\xf3i tr\u1ea3 ph\xed \u0111\u1ec3 xem th\xf4ng tin li\xean h\u1ec7",rejectClassName:"m-auto",acceptClassName:"hidden",rejectLabel:"Xem b\u1ea3ng gi\xe1",reject:function(){return window.location.href="/components/service-packages-and-payments/"}}),function(){var e,t,i,n,l,a,r;return(0,o.jsx)(o.Fragment,{children:(0,o.jsx)("div",{className:"header-social-kols-detail section-social-kols-detail",children:(0,o.jsx)("div",{className:"top-header-social",children:(0,o.jsxs)("div",{className:"grid",children:[(0,o.jsxs)("div",{className:"images text-center col col-12 md:col-4",children:[(r=null===R||void 0===R?void 0:R.resizedAvatarPath,r?Y?(0,o.jsx)(x.O,{shape:"circle",size:"5rem"}):(0,o.jsx)(s.q,{image:"https://s3.cloudfly.vn/my-kol/"+r,shape:"circle",alt:"Influx"}):(0,o.jsx)(s.q,{image:"".concat($,"/demo/images/avatar/no-avatar.png"),shape:"circle",alt:"Influx"})),(0,o.jsx)("br",{}),(0,o.jsx)("a",{className:"text-primary underline font-bold",target:"_blank",href:ye(null===R||void 0===R?void 0:R.username),title:"Xem k\xeanh",children:"Xem k\xeanh"}),(0,o.jsx)("div",{className:"text-center mt-2",children:null===R||void 0===R?void 0:R.bio}),(0,o.jsx)("a",{className:"text-center underline",target:"_blank",href:null===R||void 0===R?void 0:R.bioUrl,title:null===R||void 0===R?void 0:R.bioUrl,children:null===R||void 0===R?void 0:R.bioUrl})]}),(0,o.jsxs)("div",{className:"info col col-12 md:col-6 py-0",children:[(0,o.jsx)("h3",{className:"title-section title-name capitalize block text-primary",children:null===R||void 0===R?void 0:R.fullName}),(0,o.jsxs)("div",{className:"box-careerFields",children:[!y.tq&&!!(null===R||void 0===R?void 0:R.profileCategories)&&(null===(t=null===R||void 0===R||null===(e=R.profileCategories)||void 0===e?void 0:e.slice(0,4))||void 0===t?void 0:t.map((function(e,t){return(0,o.jsx)(h.A,{className:"mr-2 mb-2 cursor-pointer",label:e.name},t)}))),!y.tq&&!!(null===R||void 0===R?void 0:R.profileCategories)&&(null===R||void 0===R?void 0:R.profileCategories.length)>4&&(a=null===R||void 0===R||null===(i=R.profileCategories)||void 0===i?void 0:i[4].value,(0,o.jsxs)(o.Fragment,{children:[(0,o.jsx)(h.A,{className:"custom-target-icon cursor-pointer","data-pr-tooltip":a,"data-pr-position":"bottom","data-pr-at":"center top-28","data-pr-my":"center",label:"+1"},"5"),(0,o.jsx)(p.u,{target:".custom-target-icon"})]})),!!y.tq&&!!(null===R||void 0===R?void 0:R.profileCategories)&&(null===R||void 0===R||null===(n=R.profileCategories)||void 0===n?void 0:n.map((function(e,t){return(0,o.jsx)(h.A,{className:"mr-2 mb-2 cursor-pointer",label:e.name},t)})))]})]}),C&&(0,o.jsx)("div",{className:"col col-12 md:col-2",children:(0,o.jsx)(g.z,{label:"".concat((null===R||void 0===R||null===(l=R.tiktokSaveListIds)||void 0===l?void 0:l.length)>0?"\u0110\xe3 l\u01b0u":"L\u01b0u"),className:"cbutton-raisedt",onClick:function(){!me||me.accountId?ke(!0):d((0,D.PM)())}})})]})})})})}(),!R&&(0,o.jsx)(m.v,{severity:"warn",text:"Kh\xf4ng c\xf3 d\u1eef li\u1ec7u",className:"w-full mt-3 justify-content-start mb-3"}),!!R&&function(e){var t,i,n,a,r,s,c=[],d=[],u=[],m=[],h=((null===e||void 0===e?void 0:e.liveGpmMinimal)+(null===e||void 0===e?void 0:e.liveGpmMaximum))/2*(null===e||void 0===e?void 0:e.liveAvg)/1e3,g=h*(null===e||void 0===e?void 0:e.totalLive),x=((null===e||void 0===e?void 0:e.videoGpmMinimal)+(null===e||void 0===e?void 0:e.videoGpmMaximum))/2*(null===e||void 0===e?void 0:e.videoAvg)/1e3,j=x*(null===e||void 0===e?void 0:e.totalVideo),N=j+g;(null===e||void 0===e||null===(t=e.profileMeasurements)||void 0===t?void 0:t.length)>0&&(null===(s=S().orderBy(null===e||void 0===e?void 0:e.profileMeasurements,["timestamp"],["asc"]))||void 0===s||s.map((function(e){m.push(w().unix(null===e||void 0===e?void 0:e.timestamp).format("DD/MM")),c.push(null===e||void 0===e?void 0:e.viewsCount),d.push(null===e||void 0===e?void 0:e.likesCount),u.push(null===e||void 0===e?void 0:e.commentsCount)})));var k=null===e||void 0===e||null===(i=e.profileMeasurements)||void 0===i?void 0:i.reduce((function(e,t){return e+(null===t||void 0===t?void 0:t.viewsCount)}),0),y=null===e||void 0===e||null===(n=e.profileMeasurements)||void 0===n?void 0:n.reduce((function(e,t){return e+(null===t||void 0===t?void 0:t.likesCount)}),0),C=null===e||void 0===e||null===(a=e.profileMeasurements)||void 0===a?void 0:a.reduce((function(e,t){return e+(null===t||void 0===t?void 0:t.commentsCount)}),0),P=(0,v.dQ)(k?y/k:0,null===v.PQ||void 0===v.PQ?void 0:v.PQ.likeView),_=(0,v.dQ)(k?C/k:0,null===v.PQ||void 0===v.PQ?void 0:v.PQ.commentView);return(0,o.jsxs)(o.Fragment,{children:[(0,o.jsx)("div",{className:"section-social-kols-detail block-Statistical-social text-white",children:(0,o.jsxs)("div",{className:"grid",children:[(0,o.jsxs)("div",{className:"items-Statistical col col-12 md:col-6 text-center",children:["Followers",(0,o.jsx)("br",{}),(0,o.jsx)("strong",{className:"block",children:(0,v.ed)(null===e||void 0===e?void 0:e.totalFollowersCount)})]}),(0,o.jsxs)("div",{className:"items-Statistical col col-12 md:col-6 text-center",children:["T\u1ed5ng doanh thu k\xeanh",(0,o.jsx)("i",{className:"pi pi-info-circle custom-target-persent-view-video ml-2 mr-1 cursor-pointer","data-pr-tooltip":"L\u01b0\u1ee3t view trung b\xecnh c\u1ee7a 15 video g\u1ea7n nh\u1ea5t c\u1ee7a Influencer.","data-pr-position":"bottom","data-pr-at":"center top-38","data-pr-my":"center"}),(0,o.jsx)(p.u,{target:".custom-target-persent-view-video"}),(0,o.jsx)("br",{}),(0,o.jsx)("strong",{className:"block",children:N?(0,v.cP)(N):"N/A"})]})]})}),(0,o.jsxs)("div",{className:"section-social-kols-detail block-sales-social",children:[(0,o.jsx)("h3",{className:"title-section block",children:"Ch\u1ec9 s\u1ed1 b\xe1n h\xe0ng"}),(0,o.jsxs)("div",{className:"grid",children:[(0,o.jsx)("div",{className:"col col-12 md:col-4 text-center",children:(0,o.jsxs)("div",{className:"items-sales py-4 border-2 border-round-lg border-primary",children:[(0,o.jsx)("b",{children:"T\u1ed5ng doanh thu Video"}),(0,o.jsx)("i",{className:"pi pi-info-circle total-video-revenue ml-2 mr-1 cursor-pointer","data-pr-tooltip":"T\u1ed5ng doanh thu c\u1ee7a Video Tiktok c\xf3 g\u1eafn s\u1ea3n ph\u1ea9m trong 30 ng\xe0y g\u1ea7n nh\u1ea5t","data-pr-position":"bottom","data-pr-at":"center top-25","data-pr-my":"center"}),(0,o.jsx)(p.u,{target:".total-video-revenue"}),(0,o.jsx)("br",{}),(0,o.jsx)("span",{className:"block",children:j?(0,v.cP)(j):"N/A"})]})}),(0,o.jsx)("div",{className:"col col-12 md:col-4 text-center",children:(0,o.jsxs)("div",{className:"items-sales py-4 border-2 border-round-lg border-primary",children:[(0,o.jsx)("b",{children:"Doanh thu / Video"}),(0,o.jsx)("i",{className:"pi pi-info-circle total-avg-video ml-2 mr-1 cursor-pointer","data-pr-tooltip":"Doanh thu trung b\xecnh c\u1ee7a 1 Video Tiktok c\xf3 g\u1eafn s\u1ea3n ph\u1ea9m trong 30 ng\xe0y g\u1ea7n nh\u1ea5t","data-pr-position":"bottom","data-pr-at":"center top-25","data-pr-my":"center"}),(0,o.jsx)(p.u,{target:".total-avg-video"}),(0,o.jsx)("br",{}),(0,o.jsx)("span",{className:"block",children:x?(0,v.cP)(x):"N/A"})]})}),(0,o.jsx)("div",{className:"col col-12 md:col-4 text-center",children:(0,o.jsxs)("div",{className:"items-sales py-4 border-2 border-round-lg border-primary",children:[(0,o.jsx)("b",{children:"View trung b\xecnh / Video"}),(0,o.jsx)("i",{className:"pi pi-info-circle total-channel-revenue ml-2 mr-1 cursor-pointer","data-pr-tooltip":"L\u01b0\u1ee3t xem trung b\xecnh c\u1ee7a 1 Video Tiktok c\xf3 g\u1eafn s\u1ea3n ph\u1ea9m trong 30 ng\xe0y g\u1ea7n nh\u1ea5t","data-pr-position":"bottom","data-pr-at":"center top-25","data-pr-my":"center"}),(0,o.jsx)(p.u,{target:".total-channel-revenue"}),(0,o.jsx)("br",{}),(0,o.jsx)("span",{className:"block",children:(null===e||void 0===e?void 0:e.videoAvg)?(0,v.ed)(null===e||void 0===e?void 0:e.videoAvg):"N/A"})]})}),(0,o.jsx)("div",{className:"col col-12 md:col-4 text-center",children:(0,o.jsxs)("div",{className:"items-sales py-4 border-2 border-round-lg border-primary",children:[(0,o.jsx)("b",{children:"T\u1ed5ng doanh thu Live"}),(0,o.jsx)("i",{className:"pi pi-info-circle total-live-revenue ml-2 mr-1 cursor-pointer","data-pr-tooltip":"T\u1ed5ng doanh thu c\u1ee7a Livestream Tiktok c\xf3 g\u1eafn s\u1ea3n ph\u1ea9m trong 30 ng\xe0y g\u1ea7n nh\u1ea5t","data-pr-position":"bottom","data-pr-at":"center top-25","data-pr-my":"center"}),(0,o.jsx)(p.u,{target:".total-live-revenue"}),(0,o.jsx)("br",{}),(0,o.jsx)("span",{className:"block",children:g?(0,v.cP)(g):"N/A"})]})}),(0,o.jsx)("div",{className:"col col-12 md:col-4 text-center",children:(0,o.jsxs)("div",{className:"items-sales py-4 border-2 border-round-lg border-primary",children:[(0,o.jsx)("b",{children:"Doanh thu / Live"}),(0,o.jsx)("i",{className:"pi pi-info-circle total-avg-live ml-2 mr-1 cursor-pointer","data-pr-tooltip":"Doanh thu trung b\xecnh c\u1ee7a 1 phi\xean Livestream Tiktok c\xf3 g\u1eafn s\u1ea3n ph\u1ea9m trong 30 ng\xe0y g\u1ea7n nh\u1ea5t","data-pr-position":"bottom","data-pr-at":"center top-25","data-pr-my":"center"}),(0,o.jsx)(p.u,{target:".total-avg-live"}),(0,o.jsx)("br",{}),(0,o.jsx)("span",{className:"block",children:h?(0,v.cP)(h):"N/A"})]})}),(0,o.jsx)("div",{className:"col col-12 md:col-4 text-center",children:(0,o.jsxs)("div",{className:"items-sales py-4 border-2 border-round-lg border-primary",children:[(0,o.jsx)("b",{children:"View trung b\xecnh / Live"}),(0,o.jsx)("i",{className:"pi pi-info-circle total-sales ml-2 mr-1 cursor-pointer","data-pr-tooltip":"L\u01b0\u1ee3t xem trung b\xecnh c\u1ee7a 1 phi\xean Livestream Tiktok c\xf3 g\u1eafn s\u1ea3n ph\u1ea9m trong 30 ng\xe0y g\u1ea7n nh\u1ea5t","data-pr-position":"bottom","data-pr-at":"center top-25","data-pr-my":"center"}),(0,o.jsx)(p.u,{target:".total-sales"}),(0,o.jsx)("br",{}),(0,o.jsx)("span",{className:"block",children:(null===e||void 0===e?void 0:e.liveAvg)?(0,v.ed)(null===e||void 0===e?void 0:e.liveAvg):"N/A"})]})})]})]}),(0,o.jsxs)("div",{className:"relative",children:[(0,o.jsx)(Pe,{}),(0,o.jsxs)("div",{className:"section-social-kols-detail block-trend-social",children:[(0,o.jsx)("h3",{className:"title-section block",children:"Xu h\u01b0\u1edbng"}),(0,o.jsxs)(b.f,{children:[(0,o.jsx)(b.x,{header:"L\u01b0\u1ee3t xem",children:(0,o.jsx)(f.default,{dataChartLine:{data:c,label:"L\u01b0\u1ee3t xem",labels:m}})}),(0,o.jsx)(b.x,{header:"L\u01b0\u1ee3t like",children:(0,o.jsx)(f.default,{dataChartLine:{data:d,label:"L\u01b0\u1ee3t like",labels:m}})}),(0,o.jsx)(b.x,{header:"L\u01b0\u1ee3t b\xecnh lu\u1eadn",children:(0,o.jsx)(f.default,{dataChartLine:{data:u,label:"L\u01b0\u1ee3t b\xecnh lu\u1eadn",labels:m}})})]})]}),(0,o.jsxs)("div",{className:"section-social-kols-detail block-interaction-index-social",children:[(0,o.jsx)("h3",{className:"title-section block",children:"Ch\u1ec9 s\u1ed1 t\u01b0\u01a1ng t\xe1c"}),(0,o.jsx)("div",{className:"box-interaction",children:(0,o.jsxs)("div",{className:"grid",children:[(0,o.jsx)(Ce,{dataChartDoughnut:(0,l.Z)({icon:"pi-heart",title:null===v.PQ||void 0===v.PQ?void 0:v.PQ.likeView},P)}),(0,o.jsx)(Ce,{dataChartDoughnut:(0,l.Z)({icon:"pi-comments",title:null===v.PQ||void 0===v.PQ?void 0:v.PQ.commentView},_)})]})})]}),(0,o.jsxs)("div",{className:"section-social-kols-detail block-followers-social",children:[(0,o.jsx)("h3",{className:"title-section block mb-4",children:"Follower c\u1ee7a influencer"}),(0,o.jsx)("div",{className:"box-followers",children:(0,o.jsxs)("div",{className:"grid",children:[(0,o.jsx)(we,{dataChartDoughnutMulti:{element:"chartDoughnutGender",icon:"pi-user",label:"Gi\u1edbi t\xednh",data:[{value:null===e||void 0===e?void 0:e.genderFemaleRatio,name:"N\u1eef",className:"chart-bg-indigo-500",meta:"N\u1eef"},{value:null===e||void 0===e?void 0:e.genderMaleRatio,name:"Nam",className:"chart-bg-blue-500",meta:"Nam"}]}},"chartDoughnutGender"),(0,o.jsx)(we,{dataChartDoughnutMulti:{element:"chartDoughnutAge",icon:"pi-stopwatch",label:"\u0110\u1ed9 tu\u1ed5i",data:[{value:null===e||void 0===e?void 0:e.age1824Ratio,name:"18-24",className:"chart-bg-indigo-500",meta:"18-24"},{value:null===e||void 0===e?void 0:e.age2534Ratio,name:"25-34",className:"chart-bg-blue-500",meta:"25-34"},{value:null===e||void 0===e?void 0:e.ageOver35Ratio,name:"35 tr\u1edf l\xean",className:"chart-bg-green-500",meta:"35 tr\u1edf l\xean"}]}},"chartDoughnutAge")]})})]}),(null===R||void 0===R||null===(r=R.tiktokProfileVideos)||void 0===r?void 0:r.length)>0&&(0,o.jsxs)("div",{className:"section-social-kols-detail block-videos-social mb-3",children:[(0,o.jsx)("h3",{className:"title-section block mb-4",children:"Video c\xf3 g\u1eafn s\u1ea3n ph\u1ea9m"}),(0,o.jsx)("div",{className:"box-videos",children:null===R||void 0===R?void 0:R.tiktokProfileVideos.map((function(e,t){return(0,o.jsx)("div",{className:"item-videos cursor-pointer",children:(0,o.jsx)("a",{href:null===e||void 0===e?void 0:e.url,title:null===e||void 0===e?void 0:e.title,className:"text-color",target:"_blank",children:(0,o.jsxs)("div",{className:"grid",children:[(0,o.jsx)("div",{className:"col col-12 md:col-4",style:{height:"130px"},children:(0,o.jsx)("div",{className:"relative h-full",children:(0,o.jsx)("img",{style:{objectFit:"contain"},src:(null===e||void 0===e?void 0:e.thumbnailUrl)?"".concat("https://s3.cloudfly.vn/my-kol","/").concat(null===e||void 0===e?void 0:e.thumbnailUrl):"".concat($,"/layout/images/logo.jpg"),alt:null===e||void 0===e?void 0:e.title,height:130,className:"w-full border-1 border-solid surface-border border-round overflow-hidden h-full absolute left-0 top-0"})})}),(0,o.jsxs)("div",{className:"col col-12 md:col-8",children:[(0,o.jsx)("h4",{className:"mb-2 text-lg line-height-3 cut-line-2",children:null===e||void 0===e?void 0:e.title}),(0,o.jsxs)("div",{className:"social-video",children:[(0,o.jsxs)("span",{className:"mr-3 inline-block",children:[(0,o.jsx)("i",{className:"pi mr-2 vertical-align-middle pi-eye"}),(0,v.ed)(null===e||void 0===e?void 0:e.totalViewsCount)]}),(0,o.jsxs)("span",{className:"mr-3 inline-block",children:[(0,o.jsx)("i",{className:"pi mr-2 vertical-align-middle pi-heart-fill"}),null===e||void 0===e?void 0:e.totalLikesCount]})]})]})]})})},t)}))})]})]})]})}(R),function(){var e=w()(new Date).format("YYYY-MM-DD"),t=w()(null===R||void 0===R?void 0:R.lastCrawlTime).format("YYYY-MM-DD");if((null===R||void 0===R?void 0:R.lastCrawlTime)&&w()(e).isSameOrBefore(t)||!(null===R||void 0===R?void 0:R.username))return(0,o.jsx)(o.Fragment,{});var i=function(){G(!0),ce.refreshTiktokProfiles(null===R||void 0===R?void 0:R.username).then((function(e){var t;"success"==(null===e||void 0===e||null===(t=e.data)||void 0===t?void 0:t.code)&&(W(!i),ee.current.show({severity:"success",summary:"Th\xf4ng b\xe1o",detail:"D\u1eef li\u1ec7u \u0111ang \u0111\u01b0\u1ee3c c\u1eadp nh\u1eadt, vui l\xf2ng ch\u1edd 3-5 ph\xfat",life:3e3}))})).catch((function(e){ee.current.show({severity:"error",summary:"Th\xf4ng b\xe1o",detail:"L\xe0m m\u1edbi th\u1ea5y b\u1ea1i",life:3e3})})).finally((function(){return G(!1)}))};return(0,o.jsxs)("div",{className:"mb-5 font-bold flex align-items-center flex-wrap",children:["D\u1eef li\u1ec7u \u0111\u01b0\u1ee3c c\u1eadp nh\u1eadt \u0111\u1ebfn ng\xe0y ",w()(null===R||void 0===R?void 0:R.lastCrawlTime).format("DD/MM/YYYY")," ",(0,o.jsx)(g.z,{disabled:z,loading:z,onClick:function(){return i()},className:"py-2 ml-2",label:"L\xe0m m\u1edbi"})]})}(),(0,o.jsxs)("blockquote",{className:"block-pink mb-0",children:[(0,o.jsx)("span",{className:"font-bold text-red-500",children:"L\u01b0u \xfd:"})," C\xe1c s\u1ed1 li\u1ec7u \u0111\u01b0\u1ee3c \u01b0\u1edbc t\xednh d\u1ef1a tr\xean c\xe1c thu\u1eadt to\xe1n c\u1ee7a ch\xfang t\xf4i v\xe0 ch\u1ec9 mang t\xednh ch\u1ea5t tham kh\u1ea3o."]})]})}},411:function(e,t,i){i.r(t),i.d(t,{default:function(){return o}});var n=i(85893),l=i(67294),a=i(53741);function o(e){var t=e.data,i=(0,l.useState)({}),o=i[0],r=i[1],s=(0,l.useState)({}),c=s[0],d=s[1];return(0,l.useEffect)((function(){var e=getComputedStyle(document.documentElement),i={labels:[],datasets:[{data:null===t||void 0===t?void 0:t.data,backgroundColor:[e.getPropertyValue("--".concat(null===t||void 0===t?void 0:t.style,"-500")),e.getPropertyValue("--".concat(null===t||void 0===t?void 0:t.style,"-200"))],hoverBackgroundColor:[e.getPropertyValue("--".concat(null===t||void 0===t?void 0:t.style,"-400")),e.getPropertyValue("--".concat(null===t||void 0===t?void 0:t.style,"-100"))]}]};r(i),d({borderWidth:0,responsive:!0,cutout:"65%",plugins:{legend:{title:{display:!1}}}})}),[null===t||void 0===t?void 0:t.data]),(0,n.jsx)(a.k,{type:"doughnut",data:o,options:c,className:"w-full"})}},33270:function(e,t,i){i.r(t);var n=i(85893),l=i(67294),a=i(27678);i(34618);t.default=function(e){var t=e.data,i=e.dataKey;(0,l.useEffect)((function(){new a.uc("#".concat(null===t||void 0===t?void 0:t.element),{series:null===t||void 0===t?void 0:t.data},{donut:!0,donutWidth:40,startAngle:0,labelOffset:-20,labelDirection:"explode",labelInterpolationFnc:function(e,i){var n,l,a,o;return e<1?(null===t||void 0===t||null===(a=t.data)||void 0===a||null===(o=a[i])||void 0===o?void 0:o.name)+" "+"(".concat(Math.round(100*e),"%)"):(null===t||void 0===t||null===(n=t.data)||void 0===n||null===(l=n[i])||void 0===l?void 0:l.name)+" "+"(".concat(e,")}%)")}},[["screen and (min-width: 640px)",{chartPadding:30,labelOffset:40,labelDirection:"explode",labelInterpolationFnc:function(e,i){var n,l,a,o;return e<1?(null===t||void 0===t||null===(a=t.data)||void 0===a||null===(o=a[i])||void 0===o?void 0:o.name)+" "+"(".concat(Math.round(100*e),"%)"):(null===t||void 0===t||null===(n=t.data)||void 0===n||null===(l=n[i])||void 0===l?void 0:l.name)+" "+"(".concat(e,")}%)")}}],["screen and (min-width: 1024px)",{labelOffset:10,chartPadding:30,labelDirection:"explode",labelInterpolationFnc:function(e,i){var n,l,a,o;return e<1?(null===t||void 0===t||null===(a=t.data)||void 0===a||null===(o=a[i])||void 0===o?void 0:o.name)+" "+"(".concat(Math.round(100*e),"%)"):(null===t||void 0===t||null===(n=t.data)||void 0===n||null===(l=n[i])||void 0===l?void 0:l.name)+" "+"(".concat(e,")}%)")}}]])}),[]);var o;return(0,n.jsxs)(n.Fragment,{children:[(0,n.jsx)("div",{id:null===t||void 0===t?void 0:t.element,style:{height:"240px"},className:"relative cursor-pointer",children:(0,n.jsx)("div",{className:"chartCenter text-center absolute",children:(0,n.jsx)("span",{children:null===t||void 0===t?void 0:t.label})})},i),(o=null===t||void 0===t?void 0:t.data,(0,n.jsx)("ul",{className:"box-datasets-chart pl-0 my-0 justify-content-center flex list-none",children:null===o||void 0===o?void 0:o.map((function(e){return(0,n.jsxs)("li",{className:"cursor-pointer",children:[(0,n.jsx)("span",{className:"dots-chart border-circle ".concat(null===e||void 0===e?void 0:e.className)}),null===e||void 0===e?void 0:e.name]})}))}))]})}},88988:function(e,t,i){i.r(t),i.d(t,{default:function(){return r}});var n=i(29815),l=i(85893),a=i(67294),o=i(53741);function r(e){var t=e.dataChartLine,i=(0,a.useState)({}),r=i[0],s=i[1],c=(0,a.useState)({}),d=c[0],u=c[1];return(0,a.useEffect)((function(){var e=getComputedStyle(document.documentElement),i={labels:t.labels,datasets:[{data:null===t||void 0===t?void 0:t.data,label:null===t||void 0===t?void 0:t.label,fill:!1,borderColor:e.getPropertyValue("--primary-color"),tension:.4}]},l={maintainAspectRatio:!1,aspectRatio:.6,responsive:!0,borderWidth:1,elements:{point:{hoverBorderWidth:2}},plugins:{legend:{labels:!1}},scales:{x:{labels:function(e){var t;if(!e)return!1;var i=[],l=(t=Math).max.apply(t,(0,n.Z)(Object.keys(e))),a=Math.round(Object.keys(e).length/2);return i.push(null===e||void 0===e?void 0:e[0]),null===e||void 0===e||e.map((function(t,n){0!==n&&n!==l&&(n===a?i.push(null===e||void 0===e?void 0:e[a]):i.push(""))})),e.length>=l&&i.push(null===e||void 0===e?void 0:e[l]),e}(null===i||void 0===i?void 0:i.labels),grid:{display:!1}},y:{ticks:{callback:function(e,t,i){return e>=1e3?e/1e3+"k":e},maxTicksLimit:50},min:0}}};s(i),u(l)}),[t]),(0,l.jsx)(o.k,{type:"line",data:r,options:d})}}}]);