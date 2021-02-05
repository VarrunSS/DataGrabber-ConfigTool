import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, throwError, Subject, ReplaySubject } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { LoggerService } from './logger.service';
import { IBaseConfigurationDetail, IConfigurationDetail, IDbResponse } from 'src/app/models';

@Injectable({ providedIn: 'root' })
export class ConfigurationService {


  api = {
    GetAllConfigurations: 'api/Configuration/GetAllConfigurations/',
    GetConfiguration: 'api/Configuration/GetConfiguration/',
    AddConfiguration: 'api/Configuration/AddConfiguration/',
    UpdateConfiguration: 'api/Configuration/UpdateConfiguration/',
    DeleteConfiguration: 'api/Configuration/DeleteConfiguration/'
  };
  

  constructor(
    private _LoggerService: LoggerService,
    private _http: HttpClient
  ) {

  }
  
  // Observable string sources
  private allConfigurationsSubject = new Subject<IBaseConfigurationDetail[]>();

  // Observable string streams
  allConfigurations$ = this.allConfigurationsSubject.asObservable();


  getAllConfigurations() {
    this._LoggerService.log('getting all config..');
    this._http.get<IBaseConfigurationDetail[]>(this.api.GetAllConfigurations)
      .subscribe((res: IBaseConfigurationDetail[]) => {
        this.allConfigurationsSubject.next(res);
      });
  }

  getConfiguration(configGUID: string): Observable<IConfigurationDetail> {
    this._LoggerService.log(`getting config details of ${configGUID}..`);
    return this._http.get<IConfigurationDetail>(this.api.GetConfiguration + configGUID);
  }

  addConfiguration(config: IConfigurationDetail) {
    this._LoggerService.log('creating config..');
    return this._http.post<IDbResponse>(this.api.AddConfiguration, config);
  }

  updateConfiguration(config: IConfigurationDetail) {
    this._LoggerService.log('updating config..');
    return this._http.put<IDbResponse>(this.api.UpdateConfiguration + config.configGUID, config);
  }

  deleteConfiguration(config: IConfigurationDetail) {
    this._LoggerService.log('deleting config..');
    return this._http.delete<IDbResponse>(this.api.DeleteConfiguration + config.configGUID);
  }

}
