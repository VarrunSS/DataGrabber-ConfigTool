"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var http_1 = require("@angular/common/http");
var HttpErrorInterceptor = /** @class */ (function () {
    function HttpErrorInterceptor() {
    }
    HttpErrorInterceptor.prototype.intercept = function (request, next) {
        console.log('interceptor starting');
        return next.handle(request);
        //.pipe(
        //  //retry(1),
        //  catchError((error: HttpErrorResponse) => {
        //    let errorMessage = '';
        //    if (error.error instanceof ErrorEvent) {
        //      // client-side error
        //      errorMessage = `Error: ${error.error.message}`;
        //    } else {
        //      // server-side error
        //      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        //    }
        //    console.log(errorMessage);
        //    return throwError(errorMessage);
        //    //return Observable.empty<HttpEvent<any>>();
        //  })
        //)
    };
    return HttpErrorInterceptor;
}());
exports.HttpErrorInterceptor = HttpErrorInterceptor;
/**
 * Provider POJO for the interceptor
 */
exports.HttpErrorInterceptorProvider = {
    provide: http_1.HTTP_INTERCEPTORS,
    useClass: HttpErrorInterceptor,
    multi: true,
};
//# sourceMappingURL=http-error.interceptor.js.map