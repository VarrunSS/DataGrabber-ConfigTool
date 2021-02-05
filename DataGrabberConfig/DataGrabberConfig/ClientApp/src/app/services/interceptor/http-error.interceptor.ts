import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpResponse,
  HttpErrorResponse,
  HTTP_INTERCEPTORS
} from '@angular/common/http';
import { Router } from "@angular/router";
import { Observable, empty, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';


export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {


    let newRequest = request.clone();
    //// Auth check
    //if (request.headers.get('No-Auth') == "True") {
    //  // Do nothing
    //}
    ////return next.handle(request.clone());

    //if (localStorage.getItem('userToken') != null) {
    //  newRequest = request.clone({
    //    headers: request.headers.set("Authorization", "Bearer " + localStorage.getItem('userToken'))
    //  });
    //}


    return next.handle(newRequest)
      .pipe(
        //retry(1),
        catchError((error: HttpErrorResponse) => {
          let errorMessage = '';
          if (error.error instanceof ErrorEvent) {
            // client-side error
            errorMessage = `Error: ${error.error.message}`;
          } else {
            // server-side error
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
          }
          console.log(errorMessage);

          if (error.status === 401)
            this.router.navigateByUrl('/login');

        return throwError(error);
          //return Observable.empty<HttpEvent<any>>();
        })
      )
  }



  //intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

  //  return next.handle(request)
  //    .pipe(
  //      catchError((err: HttpErrorResponse) => {

  //        if (err.error instanceof Error) {
  //          // A client-side or network error occurred. Handle it accordingly.
  //          console.log('An error occurred:', err.error.message);
  //        } else {
  //          // The backend returned an unsuccessful response code.
  //          // The response body may contain clues as to what went wrong,
  //          console.log(`Backend returned code ${err.status}, body was: ${err.error}`);
  //        }

  //        return throwError(errorMessage);
  //      //return new EmptyObservable();
  //      //return Observable.empty<HttpEvent<any>>();

  //        // ...optionally return a default fallback value so app can continue (pick one)
  //        // which could be a default value
  //        //return Observable.of(new HttpResponse({
  //        //  body: [
  //        //    { name: "Default values returned by Interceptor", id: 88 },
  //        //    { name: "Default values returned by Interceptor(2)", id: 89 }
  //        //  ]
  //        //}));

  //        // or simply an empty observable
  //        //return EMPTY<HttpEvent<any>>();
  //      })
  //    );
  //}

}


/**
 * Provider POJO for the interceptor
 */
export const HttpErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: HttpErrorInterceptor,
  multi: true,
};
