"use strict";(self.webpackChunkClient=self.webpackChunkClient||[]).push([[600],{5600:(N,p,r)=>{r.r(p),r.d(p,{DashboardMedicinesModule:()=>y});var d=r(1311),m=r(6814),s=r(95),b=r(6319),_=r(4807),u=r(7021),C=r(8040),e=r(9468),g=r(4099),f=r(923);function v(n,c){1&n&&e.GkF(0)}function T(n,c){if(1&n){const o=e.EpF();e.TgZ(0,"form",8),e.NdJ("ngSubmit",function(){e.CHM(o);const t=e.oxw();return e.KtG(t.createUpdateMedicineForm.valid)}),e.TgZ(1,"div",9)(2,"div",10)(3,"div",11)(4,"label",12),e._uU(5,"Name"),e.qZA(),e._UZ(6,"app-text-input",13),e.qZA()()()()}if(2&n){const o=e.oxw();e.Q6J("formGroup",o.createUpdateMedicineForm),e.xp6(6),e.Q6J("label","Name")}}let h=(()=>{var n;class c{constructor(i,t){this.fb=i,this.medicineService=t,this.visible=!1,this.visibleChange=new e.vpe,this.medicineCreated=new e.vpe}ngOnInit(){this.intializeForm()}createUpdateMedicine(){0==this.createUpdateMedicineForm.get("id")?.value?this.createMedicine():this.updateMedicine(),this.modelToggeled(!1)}updateMedicine(){this.medicineService.updateMedicine(this.createUpdateMedicineForm.value).subscribe(i=>{this.medicineCreated.emit(i)})}createMedicine(){this.medicineService.createMedicine(this.createUpdateMedicineForm.value).subscribe(i=>{this.medicineCreated.emit(i)})}modelToggeled(i){this.visible=i,this.visibleChange.emit(this.visible)}intializeForm(){this.createUpdateMedicineForm=this.fb.group({id:[0,s.kI.required],name:["",s.kI.required]})}}return(n=c).\u0275fac=function(i){return new(i||n)(e.Y36(s.qu),e.Y36(g.A))},n.\u0275cmp=e.Xpm({type:n,selectors:[["app-medcine-modal"]],inputs:{visible:"visible"},outputs:{visibleChange:"visibleChange",medicineCreated:"medicineCreated"},decls:15,vars:5,consts:[["id","medicineModal","size","xl","scrollable","",3,"visible","visibleChange"],["medicineModal",""],["cModalTitle",""],["cButtonClose","",3,"cModalToggle"],[4,"ngTemplateOutlet"],["cButton","","color","secondary",3,"cModalToggle"],["cButton","","color","primary",3,"disabled","click"],["medicineCotent",""],["cForm","","autocomplete","off",3,"formGroup","ngSubmit"],[1,"row"],[1,"col"],[1,"mb-3"],["cLabel",""],["icon","","formControlName","name",3,"label"]],template:function(i,t){if(1&i&&(e.TgZ(0,"c-modal",0,1),e.NdJ("visibleChange",function(l){return t.modelToggeled(l)}),e.TgZ(2,"c-modal-header")(3,"h5",2),e._uU(4,"Medicine"),e.qZA(),e._UZ(5,"button",3),e.qZA(),e.TgZ(6,"c-modal-body"),e.YNc(7,v,1,0,"ng-container",4),e.qZA(),e.TgZ(8,"c-modal-footer")(9,"button",5),e._uU(10," Close "),e.qZA(),e.TgZ(11,"button",6),e.NdJ("click",function(){return t.createUpdateMedicineForm.valid&&t.createUpdateMedicine()}),e._uU(12,"Save changes"),e.qZA()()(),e.YNc(13,T,7,2,"ng-template",null,7,e.W1O)),2&i){const a=e.MAs(1),l=e.MAs(14);e.Q6J("visible",t.visible),e.xp6(5),e.Q6J("cModalToggle",a.id),e.xp6(2),e.Q6J("ngTemplateOutlet",l),e.xp6(2),e.Q6J("cModalToggle",a.id),e.xp6(2),e.Q6J("disabled",!t.createUpdateMedicineForm.valid)}},dependencies:[m.tP,s._Y,s.JJ,s.JL,f.t,d.Hq3,d.PFQ,d.$_X,d.eFW,s.sg,s.u,d.KF4,d.zS7,d.YI7,d.Rbl,d.vPP,d.Ntb]}),c})();var M=r(1272),Z=r(6084);function F(n,c){if(1&n){const o=e.EpF();e.TgZ(0,"tr",14),e.NdJ("click",function(){const a=e.CHM(o).$implicit,l=e.oxw(2);return e.KtG(l.openModalAndSetMedicine(a))}),e.TgZ(1,"td"),e._uU(2),e.qZA(),e.TgZ(3,"td"),e._uU(4),e.qZA(),e.TgZ(5,"td",15)(6,"button",16),e.NdJ("click",function(t){const l=e.CHM(o).$implicit,D=e.oxw(2);return e.KtG(D.deleteMedicine(l.id,t))}),e.O4$(),e._UZ(7,"svg",17),e.qZA()()()}if(2&n){const o=c.$implicit;e.xp6(2),e.Oqu(o.id),e.xp6(2),e.Oqu(o.name)}}function x(n,c){if(1&n&&(e.TgZ(0,"table",11)(1,"thead")(2,"tr")(3,"th",12),e._uU(4,"Id"),e.qZA(),e.TgZ(5,"th",12),e._uU(6,"Name"),e.qZA(),e._UZ(7,"th",12),e.qZA()(),e.TgZ(8,"tbody"),e.YNc(9,F,8,2,"tr",13),e.qZA()()),2&n){const o=e.oxw();e.xp6(9),e.Q6J("ngForOf",o.medicines)}}function A(n,c){if(1&n){const o=e.EpF();e.TgZ(0,"div",18)(1,"app-pagination",19),e.NdJ("ngModelChange",function(t){e.CHM(o);const a=e.oxw();return e.KtG(a.pagination.currentPage=t)})("pageChanged",function(t){e.CHM(o);const a=e.oxw();return e.KtG(a.pageChanged(t))}),e.qZA()()}if(2&n){const o=e.oxw();e.xp6(1),e.Q6J("boundaryLinks",!0)("totalItems",o.pagination.totalItems)("itemsPerPage",o.pagination.itemsPerPage)("ngModel",o.pagination.currentPage)}}const J=[{path:"",component:(()=>{var n;class c{constructor(i,t){this.iconSetService=i,this.medicineService=t,this.medicines=[],this.medicineParams={pageNumber:1,pageSize:15},this.pagination=null,this.modalVisibility=!1,i.icons={...C.y}}ngOnInit(){this.getMedicines()}openAndReset(){this.medicineModal.intializeForm(),this.modalVisibility=!this.modalVisibility}openModal(){this.modalVisibility=!this.modalVisibility}openModalAndSetMedicine(i){this.mapMedicineToForm(i),this.openModal()}mapMedicineToForm(i){this.medicineModal.createUpdateMedicineForm.get("id")?.setValue(i.id),this.medicineModal.createUpdateMedicineForm.get("name")?.setValue(i.name)}getMedicines(){this.medicineService.getMedicines(this.medicineParams).subscribe(i=>{this.medicines=i.result,this.pagination=i.pagination})}pageChanged(i){this.medicineParams.pageNumber=i,this.getMedicines()}resetFilters(){this.medicineParams=this.medicineService.resetParams(),this.getMedicines()}deleteMedicine(i,t){t.stopPropagation(),this.medicineService.deleteMedicine(i).subscribe({next:a=>{},error:a=>{console.error(a)}})}medicineCreated(i){this.getMedicines()}}return(n=c).\u0275fac=function(i){return new(i||n)(e.Y36(M.uk),e.Y36(g.A))},n.\u0275cmp=e.Xpm({type:n,selectors:[["app-dashboard-medicines"]],viewQuery:function(i,t){if(1&i&&e.Gf(h,5),2&i){let a;e.iGM(a=e.CRH())&&(t.medicineModal=a.first)}},decls:15,vars:4,consts:[["colorScheme","dark","expand","lg",1,"bg-dark"],["navbar","",1,"d-flex","flex-column","gap-2","flex-lg-row","justify-content-between","w-100","px-5"],["cButton","","color","light",3,"click"],[1,""],["cIcon","","name","cilPlus",1,"border","rounded","border-dark"],["cForm","","role","search",1,"d-flex",3,"submit"],["cFormControl","","type","search","placeholder","Search","name","searchTerm","aria-label","Search",1,"me-2",3,"ngModel","ngModelChange"],["cButton","","color","light","variant","outline","type","submit"],["cTable","",4,"ngIf"],["class","d-flex justify-content-center",4,"ngIf"],[3,"visible","visibleChange","medicineCreated"],["cTable",""],["scope","col"],["class","user-table",3,"click",4,"ngFor","ngForOf"],[1,"user-table",3,"click"],[1,"pt-1"],["cButton","","color","danger",1,"btn-m",3,"click"],["cIcon","","name","cil-trash"],[1,"d-flex","justify-content-center"],[3,"boundaryLinks","totalItems","itemsPerPage","ngModel","ngModelChange","pageChanged"]],template:function(i,t){1&i&&(e.TgZ(0,"c-navbar",0)(1,"div",1)(2,"button",2),e.NdJ("click",function(){return t.openAndReset()}),e.TgZ(3,"span",3),e.O4$(),e._UZ(4,"svg",4),e._uU(5," Add new medicine"),e.qZA()(),e.kcU(),e.TgZ(6,"form",5),e.NdJ("submit",function(){return t.getMedicines()}),e.TgZ(7,"input",6),e.NdJ("ngModelChange",function(l){return t.medicineParams.searchTerm=l}),e.qZA(),e.TgZ(8,"button",7),e._uU(9,"Search"),e.qZA()(),e.TgZ(10,"button",2),e.NdJ("click",function(){return t.resetFilters()}),e._uU(11,"Reset Filters"),e.qZA()()(),e.YNc(12,x,10,1,"table",8),e.YNc(13,A,2,4,"div",9),e.TgZ(14,"app-medcine-modal",10),e.NdJ("visibleChange",function(l){return t.modalVisibility=l})("medicineCreated",function(l){return t.medicineCreated(l)}),e.qZA()),2&i&&(e.xp6(7),e.Q6J("ngModel",t.medicineParams.searchTerm),e.xp6(5),e.Q6J("ngIf",t.medicines&&t.medicines.length>0),e.xp6(1),e.Q6J("ngIf",t.pagination),e.xp6(1),e.Q6J("visible",t.modalVisibility))},dependencies:[d.auY,d.SB8,m.sg,m.O5,s._Y,s.Fj,s.JJ,s.JL,s.On,s.F,M.ar,d.Hq3,d.$_X,d.oHf,Z.Q,h]}),c})(),data:{title:"Medicines"}}];let U=(()=>{var n;class c{}return(n=c).\u0275fac=function(i){return new(i||n)},n.\u0275mod=e.oAB({type:n}),n.\u0275inj=e.cJS({imports:[u.Bz.forChild(J),u.Bz]}),c})(),y=(()=>{var n;class c{}return(n=c).\u0275fac=function(i){return new(i||n)},n.\u0275mod=e.oAB({type:n}),n.\u0275inj=e.cJS({imports:[U,d.U$I,d.W4s,d.e$z,m.ez,d.u3b,s.u5,b.c,d.m81,d.zkK,_.A0]}),c})()}}]);