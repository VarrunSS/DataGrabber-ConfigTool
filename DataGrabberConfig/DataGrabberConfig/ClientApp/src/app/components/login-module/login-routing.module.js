"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var login_module_1 = require("./login.module");
var routes = [
    {
        path: '', component: login_module_1.UserLoginComponent,
        children: [{ path: 'sign-up', component: login_module_1.SignUpComponent }]
    },
    {
        path: '', component: login_module_1.UserLoginComponent,
        children: [{ path: 'login', component: login_module_1.SignInComponent }]
    }
];
//@NgModule({
//  imports: [RouterModule.forRoot(routes)],
//  exports: [RouterModule]
//})
//export class LoginRoutingModule { }
exports.LoginRoutingModule = router_1.RouterModule.forChild(routes);
//# sourceMappingURL=login-routing.module.js.map