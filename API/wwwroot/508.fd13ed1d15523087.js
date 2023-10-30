"use strict";(self.webpackChunkClient=self.webpackChunkClient||[]).push([[508],{2853:(M,h,i)=>{i.d(h,{y:()=>e});const e=[{value:"male",display:"Male"},{value:"female",display:"Female"}]},1520:(M,h,i)=>{i.d(h,{l:()=>C});var e=i(5516),s=i(7398),p=i(9468),U=i(9862),Z=i(3934);let C=(()=>{var c;class u{constructor(d,o){this.http=d,this.userService=o,this.baseUrl=e.N.apiUrl}createUser(d){return this.http.post(this.baseUrl+"admin/createUser/",d).pipe((0,s.U)(o=>(this.userService.invalidateUsersCache(),o)))}updateUser(d){return this.http.put(this.baseUrl+"admin/updateUser/",d).pipe((0,s.U)(o=>(this.userService.invalidateUsersCache(),o)))}toggleLockout(d){return this.http.put(this.baseUrl+"admin/toggleLockUser/"+d,{})}changeRole(d,o){return this.http.put(this.baseUrl+"admin/changeUserRole/"+d,o)}}return(c=u).\u0275fac=function(d){return new(d||c)(p.LFG(U.eN),p.LFG(Z.K))},c.\u0275prov=p.Yz7({token:c,factory:c.\u0275fac,providedIn:"root"}),u})()},4726:(M,h,i)=>{i.d(h,{T:()=>U,r:()=>Z});var e=i(9862),s=i(7398);class p{constructor(){this.result=null,this.pagination=null}}function U(C,c,u){const m=new p;return u.get(C,{observe:"response",params:c}).pipe((0,s.U)(d=>(m.result=d.body,null!==d.headers.get("Pagination")&&(m.pagination=JSON.parse(d.headers.get("Pagination"))),m)))}function Z(C,c){let u=new e.LE;return u=u.append("pageNumber",C.toString()),u=u.append("pageSize",c.toString()),u}},3934:(M,h,i)=>{i.d(h,{K:()=>u});var e=i(7490),s=i(5516),p=i(4726),U=i(2096),Z=i(7398),C=i(9468),c=i(9862);let u=(()=>{var m;class d{constructor(t){this.http=t,this.baseUrl=s.N.apiUrl,this.userParams=new e.M({pageNumber:1,pageSize:15}),this.memberCache=new Map}resetUserParams(){return this.userParams=new e.M,this.userParams}getUserData(t){const g=Object.values(t).join("-"),P=this.memberCache.get(g);if(P)return(0,U.of)(P);let f=(0,p.r)(t.pageNumber,t.pageSize);return f=f.append("orderBy",t.orderBy),f=f.append("order",t.order),""!==t.searchTerm.trim()&&(f=f.append("searchTerm",t.searchTerm.trim())),""!==t.gender.trim()&&(f=f.append("gender",t.gender.trim())),null!==t.doctorSpecialityId&&(f=f.append("doctorSpecialityId",t.doctorSpecialityId)),""!==t.roleName.trim()&&(f=f.append("roleName",t.roleName.trim())),(0,p.T)(this.baseUrl+"user/all/",f,this.http).pipe((0,Z.U)(E=>(this.memberCache.set(g,E),E)))}getUsers(t){return this.getUserData(t)}getUser(t){const g=[...this.memberCache.values()].reduce((P,f)=>P.concat(f.result),[]).find(P=>P.id==t);return g?(0,U.of)(g):this.http.get(this.baseUrl+"user/"+t)}setUserParams(t){this.userParams=t}updateUser(t){return this.http.put(this.baseUrl+"user/",t)}uploadImage(t){const g=new FormData;g.append("file",t.file),g.append("userId",t.userId.toString()),g.append("category",t.category);let P=new Date(t.imageDate);return g.append("imageDate",P.toISOString()),g.append("type",t.type),g.append("organ",t.organ),this.http.post(this.baseUrl+"user/uploadImage",g)}getUserImages(t){return this.http.get(this.baseUrl+"user/userImages/"+t)}invalidateUsersCache(){this.memberCache.clear()}}return(m=d).\u0275fac=function(t){return new(t||m)(C.LFG(c.eN))},m.\u0275prov=C.Yz7({token:m,factory:m.\u0275fac,providedIn:"root"}),d})()},2557:(M,h,i)=>{i.d(h,{Z:()=>e});class e{constructor(){this.pageNumber=1,this.pageSize=15}}},7490:(M,h,i)=>{i.d(h,{M:()=>s});var e=i(2557);class s extends e.Z{constructor(U){super(),this.orderBy="date",this.order="asc",this.gender="",this.searchTerm="",this.roleName="",this.doctorSpecialityId=null,Object.assign(this,U)}}},1940:(M,h,i)=>{i.d(h,{i:()=>e});class e{constructor(p){this.ticks=p}get days(){return Math.floor(this.ticks/864e5)}get hours(){return Math.floor(this.ticks%864e5/36e5)}get minutes(){return Math.floor(this.ticks%36e5/6e4)}get seconds(){return Math.floor(this.ticks%6e4/1e3)}get milliseconds(){return this.ticks%1e3}add(p){return new e(this.ticks+p.ticks)}subtract(p){return new e(this.ticks-p.ticks)}compareTo(p){return this.ticks>p.ticks?1:this.ticks<p.ticks?-1:0}equals(p){return this.ticks===p.ticks}toString(){return`${this.hours.toString().padStart(2,"0")}:${this.minutes.toString().padStart(2,"0")}:${this.seconds.toString().padStart(2,"0")}.${this.milliseconds.toString().padStart(7,"0")}`}}},2527:(M,h,i)=>{i.d(h,{n:()=>L});var e=i(9468),s=i(95),p=i(2853),U=i(3348),Z=i(1940),C=i(1520),c=i(1311),u=i(6814),m=i(923);function d(r,_){1&r&&e.GkF(0)}function o(r,_){if(1&r&&(e.TgZ(0,"option",27),e._uU(1),e.qZA()),2&r){const n=_.$implicit;e.Q6J("value",n.value),e.xp6(1),e.hij(" ",n.display," ")}}function t(r,_){if(1&r&&(e.TgZ(0,"option",27),e._uU(1),e.qZA()),2&r){const n=_.$implicit;e.Q6J("value",n),e.xp6(1),e.hij(" ",n," ")}}function g(r,_){if(1&r&&(e.TgZ(0,"option",27),e._uU(1),e.qZA()),2&r){const n=_.$implicit;e.Q6J("value",n.id),e.xp6(1),e.hij(" ",n.name," ")}}function P(r,_){if(1&r&&(e.TgZ(0,"div",28)(1,"label",12),e._uU(2,"Speciality"),e.qZA(),e.TgZ(3,"select",29)(4,"option",24),e._uU(5,"Speciality"),e.qZA(),e.YNc(6,g,2,2,"option",22),e.qZA()()),2&r){const n=e.oxw(2);e.xp6(6),e.Q6J("ngForOf",n.specialityList)}}function f(r,_){1&r&&(e.TgZ(0,"div",9)(1,"div",10)(2,"div",11)(3,"label",12),e._uU(4,"Price Visit"),e.qZA(),e._UZ(5,"app-text-input",30),e.qZA()(),e.TgZ(6,"div",10)(7,"div",11)(8,"label",12),e._uU(9,"Price Revisit"),e.qZA(),e._UZ(10,"app-text-input",31),e.qZA()()()),2&r&&(e.xp6(5),e.Q6J("label","Price Visit"),e.xp6(5),e.Q6J("label","Price Visit"))}function O(r,_){if(1&r&&(e.TgZ(0,"option",27),e._uU(1),e.qZA()),2&r){const n=_.$implicit;e.Q6J("value",n.timeSpanValue),e.xp6(1),e.Oqu(n.displayValue)}}function E(r,_){1&r&&(e.TgZ(0,"div",10)(1,"div",43),e._uU(2,"start time must be less than End time"),e.qZA()())}function I(r,_){if(1&r&&(e.TgZ(0,"option",27),e._uU(1),e.qZA()),2&r){const n=_.$implicit;e.Q6J("value",n.timeSpanValue),e.xp6(1),e.Oqu(n.displayValue)}}function x(r,_){1&r&&(e.TgZ(0,"div",10)(1,"div",43),e._uU(2,"End time is required when start time is selected."),e.qZA()())}function N(r,_){1&r&&(e.TgZ(0,"div",10)(1,"div",43),e._uU(2,"End time must be greater than start time"),e.qZA()())}function y(r,_){if(1&r&&(e.TgZ(0,"div",34)(1,"div",35)(2,"div",36)(3,"div",37),e._uU(4),e.qZA()(),e.TgZ(5,"div",10)(6,"select",38)(7,"option",39),e._uU(8,"Select a start time"),e.qZA(),e.YNc(9,O,2,2,"option",40),e.qZA(),e.YNc(10,E,3,0,"div",41),e.qZA(),e.TgZ(11,"div",10)(12,"select",42)(13,"option",39),e._uU(14,"Select an end time"),e.qZA(),e.YNc(15,I,2,2,"option",40),e.qZA(),e.YNc(16,x,3,0,"div",41),e.YNc(17,N,3,0,"div",41),e.qZA()()()),2&r){const n=_.$implicit,l=_.index,a=e.oxw(3);e.Q6J("formGroupName",l),e.xp6(4),e.Oqu(a.getDayOfWeekLabel(n.get("dayOfWeek").value)),e.xp6(5),e.Q6J("ngForOf",a.getHalfHourIntervals())("ngForTrackBy",a.trackByFn),e.xp6(1),e.Q6J("ngIf",n.get("startTime").hasError("startTimeInvalid")),e.xp6(5),e.Q6J("ngForOf",a.getHalfHourIntervals())("ngForTrackBy",a.trackByFn),e.xp6(1),e.Q6J("ngIf",n.get("endTime").hasError("endTimeRequired")),e.xp6(1),e.Q6J("ngIf",n.get("endTime").hasError("endTimeInvalid"))}}function D(r,_){if(1&r&&(e.TgZ(0,"div",9)(1,"div",10)(2,"div",11)(3,"div",32),e.YNc(4,y,18,9,"div",33),e.qZA()()()()),2&r){const n=e.oxw(2);e.xp6(4),e.Q6J("ngForOf",n.createUser.controls.doctorWorkingHours.controls)}}function S(r,_){if(1&r&&(e.TgZ(0,"li"),e._uU(1),e.qZA()),2&r){const n=_.$implicit;e.xp6(1),e.hij(" ",n," ")}}function J(r,_){if(1&r&&(e.TgZ(0,"div",9)(1,"ul",43),e.YNc(2,S,2,1,"li",44),e.qZA()()),2&r){const n=e.oxw(2);e.xp6(2),e.Q6J("ngForOf",n.validationErrors)}}function k(r,_){if(1&r){const n=e.EpF();e.TgZ(0,"form",8),e.NdJ("ngSubmit",function(){e.CHM(n);const a=e.oxw();return e.KtG(a.createUser.valid)}),e.TgZ(1,"div",9)(2,"div",10)(3,"div",11)(4,"label",12),e._uU(5,"User Name"),e.qZA(),e._UZ(6,"app-text-input",13),e.qZA()(),e.TgZ(7,"div",10)(8,"div",11)(9,"label",12),e._uU(10,"Email"),e.qZA(),e._UZ(11,"app-text-input",14),e.qZA()()(),e.TgZ(12,"div",9)(13,"div",10)(14,"div",11)(15,"label",12),e._uU(16,"Age"),e.qZA(),e._UZ(17,"app-text-input",15),e.qZA()(),e.TgZ(18,"div",10)(19,"div",11)(20,"label",12),e._uU(21,"Full Name"),e.qZA(),e._UZ(22,"app-text-input",16),e.qZA()()(),e.TgZ(23,"div",9)(24,"div",10)(25,"div",11)(26,"label",12),e._uU(27,"Password"),e.qZA(),e._UZ(28,"app-text-input",17),e.qZA()(),e.TgZ(29,"div",10)(30,"div",11)(31,"label",12),e._uU(32,"Confirm Password"),e.qZA(),e._UZ(33,"app-text-input",18),e.qZA()()(),e.TgZ(34,"div",9)(35,"div",10)(36,"div",11)(37,"label",12),e._uU(38,"Phone Number"),e.qZA(),e._UZ(39,"app-text-input",19),e.qZA()(),e.TgZ(40,"div",10)(41,"div",11)(42,"label",12),e._uU(43,"Gender"),e.qZA(),e.TgZ(44,"select",20)(45,"option",21),e._uU(46,"Gender"),e.qZA(),e.YNc(47,o,2,2,"option",22),e.qZA()()()(),e.TgZ(48,"div",9)(49,"div",10)(50,"label",12),e._uU(51,"Role"),e.qZA(),e.TgZ(52,"select",23)(53,"option",24),e._uU(54,"Role"),e.qZA(),e.YNc(55,t,2,2,"option",22),e.qZA()(),e.YNc(56,P,7,1,"div",25),e.qZA(),e.YNc(57,f,11,2,"div",26),e.YNc(58,D,5,1,"div",26),e.YNc(59,J,3,1,"div",26),e.qZA()}if(2&r){const n=e.oxw();e.Q6J("formGroup",n.createUser),e.xp6(6),e.Q6J("label","User Name"),e.xp6(5),e.Q6J("label","Email"),e.xp6(6),e.Q6J("label","Age"),e.xp6(5),e.Q6J("label","Full Name"),e.xp6(6),e.Q6J("label","Password"),e.xp6(5),e.Q6J("label","Confirm Password"),e.xp6(6),e.Q6J("label","Phone Number"),e.xp6(8),e.Q6J("ngForOf",n.genderList),e.xp6(8),e.Q6J("ngForOf",n.roles),e.xp6(1),e.Q6J("ngIf","Doctor"==n.createUser.value.role),e.xp6(1),e.Q6J("ngIf","Doctor"==n.createUser.value.role),e.xp6(1),e.Q6J("ngIf","Doctor"==n.createUser.value.role),e.xp6(1),e.Q6J("ngIf",n.validationErrors.length>0)}}let L=(()=>{var r;class _{constructor(l,a){this.fb=l,this.adminService=a,this.visible=!1,this.visibleChange=new e.vpe,this.validationErrors=[],this.roles=U.T,this.specialityList=[],this.genderList=p.y,this.userCreated=new e.vpe,this.selectedRole=""}ngOnInit(){this.intializeForm()}modelToggeled(l){this.visible=l,this.visibleChange.emit(this.visible)}intializeForm(){this.createUser=this.fb.group({username:["",s.kI.required],email:["",s.kI.required],age:["",s.kI.required],fullName:["",s.kI.required],password:["",s.kI.required],confirmPassword:["",s.kI.required],phoneNumber:["",s.kI.required],gender:["",s.kI.required],role:[this.selectedRole,s.kI.required],doctorSpecialityId:[""],priceVisit:[0],priceRevisit:[0],doctorWorkingHours:this.fb.array([])});for(let l=0;l<7;l++){const a=this.fb.group({doctorId:[0],dayOfWeek:[l],startTime:[""],endTime:[""]}),T=a.get("startTime"),v=a.get("endTime");T?.valueChanges.subscribe(b=>{const A=v?.value;b&&!A?v?.setErrors({endTimeRequired:!0}):b&&A&&b>=A?T?.setErrors({startTimeInvalid:!0}):v?.setErrors(null)}),v?.valueChanges.subscribe(b=>{const A=T?.value;v?.setErrors(A&&b&&A>=b?{endTimeInvalid:!0}:null)}),this.createUser.controls.doctorWorkingHours.push(a)}}getDayOfWeekLabel(l){switch(l){case 0:return"Sunday";case 1:return"Monday";case 2:return"Tuesday";case 3:return"Wednesday";case 4:return"Thursday";case 5:return"Friday";case 6:return"Saturday";default:return""}}getHalfHourIntervals(){const l=[];for(let a=0;a<24;a++)for(let T=0;T<60;T+=30){const v=a>=12?"PM":"AM",F=`${(a%12==0?12:a%12).toString().padStart(2,"0")}:${T.toString().padStart(2,"0")} ${v}`,R=new Z.i(60*a*60*1e3+60*T*1e3).toString();l.push({displayValue:F,timeSpanValue:R})}return l}trackByFn(l,a){return a.timeSpanValue}submitCreateUserForm(){this.createUserModel=this.mapFormToCreateUser(),this.adminService.createUser(this.createUserModel).subscribe({next:l=>{this.modelToggeled(!1),this.userCreated.emit(l)},error:l=>{this.validationErrors=l}})}mapFormToCreateUser(){let l=this.createUser.get("doctorWorkingHours")?.value;l=l.filter(v=>""!==v.startTime&&""!==v.endTime);const a={username:this.createUser.get("username")?.value,email:this.createUser.get("email")?.value,gender:this.createUser.get("gender")?.value,age:+this.createUser.get("age")?.value,fullName:this.createUser.get("fullName")?.value,phoneNumber:this.createUser.get("phoneNumber")?.value,password:this.createUser.get("password")?.value,role:this.createUser.get("role")?.value};if(l.length>0&&(a.doctorWorkingHours=l,console.log(a.doctorWorkingHours)),"Doctor"===this.createUser.get("role")?.value){const v=+this.createUser.get("doctorSpecialityId")?.value,b=+this.createUser.get("priceVisit")?.value,A=+this.createUser.get("priceRevisit")?.value;v&&b&&A&&(a.doctorSpecialityId=v,a.priceVisit=b,a.priceRevisit=A)}return a}}return(r=_).\u0275fac=function(l){return new(l||r)(e.Y36(s.qu),e.Y36(C.l))},r.\u0275cmp=e.Xpm({type:r,selectors:[["app-add-user-modal"]],inputs:{visible:"visible",specialityList:"specialityList",selectedRole:"selectedRole"},outputs:{visibleChange:"visibleChange",userCreated:"userCreated"},decls:15,vars:5,consts:[["id","scrollableLongContentModal","size","xl","scrollable","",3,"visible","visibleChange"],["scrollableLongContentModal",""],["cModalTitle",""],["cButtonClose","",3,"cModalToggle"],[4,"ngTemplateOutlet"],["cButton","","color","secondary",3,"cModalToggle"],["cButton","","color","primary",3,"disabled","click"],["longContent",""],["cForm","","autocomplete","off",3,"formGroup","ngSubmit"],[1,"row"],[1,"col"],[1,"mb-3"],["cLabel",""],["icon","","formControlName","username",3,"label"],["icon","","formControlName","email",3,"label"],["icon","","formControlName","age",3,"label"],["icon","","formControlName","fullName",3,"label"],["type","password","icon","","formControlName","password",3,"label"],["type","password","icon","","formControlName","confirmPassword",3,"label"],["icon","","formControlName","phoneNumber",3,"label"],["name","gender","aria-label","gender","formControlName","gender","cSelect",""],["value","","disabled",""],[3,"value",4,"ngFor","ngForOf"],["name","role","aria-label","role","cSelect","","formControlName","role"],["disabled","","value","","selected",""],["class","col mb-3",4,"ngIf"],["class","row",4,"ngIf"],[3,"value"],[1,"col","mb-3"],["name","doctorSpecialityId","cSelect","","formControlName","doctorSpecialityId"],["type","number","icon","","formControlName","priceVisit",3,"label"],["type","number","icon","","formControlName","priceRevisit",3,"label"],["formArrayName","doctorWorkingHours"],[3,"formGroupName",4,"ngFor","ngForOf"],[3,"formGroupName"],[1,"row","mb-2"],[1,"col-2"],[1,"text-dark","fw-bold"],["formControlName","startTime",1,"form-select"],["value","","selected",""],[3,"value",4,"ngFor","ngForOf","ngForTrackBy"],["class","col",4,"ngIf"],["formControlName","endTime",1,"form-select"],[1,"text-danger"],[4,"ngFor","ngForOf"]],template:function(l,a){if(1&l&&(e.TgZ(0,"c-modal",0,1),e.NdJ("visibleChange",function(v){return a.modelToggeled(v)}),e.TgZ(2,"c-modal-header")(3,"h5",2),e._uU(4,"Modal title"),e.qZA(),e._UZ(5,"button",3),e.qZA(),e.TgZ(6,"c-modal-body"),e.YNc(7,d,1,0,"ng-container",4),e.qZA(),e.TgZ(8,"c-modal-footer")(9,"button",5),e._uU(10," Close "),e.qZA(),e.TgZ(11,"button",6),e.NdJ("click",function(){return a.createUser.valid&&a.submitCreateUserForm()}),e._uU(12,"Save changes"),e.qZA()()(),e.YNc(13,k,60,14,"ng-template",null,7,e.W1O)),2&l){const T=e.MAs(1),v=e.MAs(14);e.Q6J("visible",a.visible),e.xp6(5),e.Q6J("cModalToggle",T.id),e.xp6(2),e.Q6J("ngTemplateOutlet",v),e.xp6(2),e.Q6J("cModalToggle",T.id),e.xp6(2),e.Q6J("disabled",!a.createUser.valid)}},dependencies:[c.Hq3,c.PFQ,c.$_X,c.eFW,c.nqR,s._Y,s.YN,s.Kr,s.EJ,s.JJ,s.JL,s.sg,s.u,s.x0,s.CE,u.sg,u.O5,u.tP,c.KF4,c.zS7,c.YI7,c.Rbl,c.vPP,c.Ntb,m.t]}),_})()},6084:(M,h,i)=>{i.d(h,{Q:()=>c});var e=i(9468),s=i(95),p=i(6814),U=i(1311);function Z(u,m){if(1&u){const d=e.EpF();e.TgZ(0,"li",4)(1,"button",2),e.NdJ("click",function(){const g=e.CHM(d).index,P=e.oxw();return e.KtG(P.onPageChange(g+1))}),e._uU(2),e.qZA()()}if(2&u){const d=m.index,o=e.oxw();e.Q6J("active",o.currentPage===d+1),e.xp6(2),e.Oqu(d+1)}}const C=function(){return[]};let c=(()=>{var u;class m{constructor(){this.totalItems=0,this.itemsPerPage=0,this.boundaryLinks=!1,this.pageChanged=new e.vpe,this.currentPage=0,this.totalPages=0,this.onChange=()=>{},this.onTouched=()=>{}}ngOnInit(){this.totalPages=Math.ceil(this.totalItems/this.itemsPerPage)}writeValue(o){this.currentPage=o}registerOnChange(o){this.onChange=o}registerOnTouched(o){this.onTouched=o}setDisabledState(o){}updatePage(o){this.currentPage=o,this.pageChanged.emit(o),this.onChange(o),this.onTouched()}goToFirstPage(){this.updatePage(1)}onPageChange(o){this.updatePage(o)}onPrevNextChange(o){this.updatePage(o?this.currentPage+1:this.currentPage-1)}goToLastPage(){this.updatePage(this.totalPages)}}return(u=m).\u0275fac=function(o){return new(o||u)},u.\u0275cmp=e.Xpm({type:u,selectors:[["app-pagination"]],inputs:{totalItems:"totalItems",itemsPerPage:"itemsPerPage",boundaryLinks:"boundaryLinks"},outputs:{pageChanged:"pageChanged"},features:[e._Bn([{provide:s.JU,useExisting:(0,e.Gpc)(()=>u),multi:!0}])],decls:14,vars:6,consts:[["aria-label","Page navigation"],["cPageItem","",3,"disabled"],["cPageLink","",3,"click"],["cPageItem","",3,"active",4,"ngFor","ngForOf"],["cPageItem","",3,"active"]],template:function(o,t){1&o&&(e.TgZ(0,"c-pagination",0)(1,"li",1)(2,"a",2),e.NdJ("click",function(){return t.goToFirstPage()}),e._uU(3,"\xab"),e.qZA()(),e.TgZ(4,"li",1)(5,"button",2),e.NdJ("click",function(){return t.onPrevNextChange(!1)}),e._uU(6,"\u2039"),e.qZA()(),e.YNc(7,Z,3,2,"li",3),e.TgZ(8,"li",1)(9,"button",2),e.NdJ("click",function(){return t.onPrevNextChange(!0)}),e._uU(10,"\u203a"),e.qZA()(),e.TgZ(11,"li",1)(12,"a",2),e.NdJ("click",function(){return t.goToLastPage()}),e._uU(13,"\xbb"),e.qZA()()()),2&o&&(e.xp6(1),e.Q6J("disabled",1===t.currentPage),e.xp6(3),e.Q6J("disabled",1===t.currentPage),e.xp6(3),e.Q6J("ngForOf",e.DdM(5,C).constructor(t.totalPages)),e.xp6(1),e.Q6J("disabled",t.currentPage===t.totalPages),e.xp6(3),e.Q6J("disabled",t.currentPage===t.totalPages))},dependencies:[p.sg,U.Qmh,U.YHm,U.QtL]}),m})()}}]);