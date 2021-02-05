import { Component, Inject, OnInit } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, NavigationExtras } from '@angular/router';
import { DOCUMENT } from "@angular/common";
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

import { ConfigurationService } from 'src/app/services';
import { IConfigurationDetail, IDbResponse, ModalMessage } from 'src/app/models';
import { ModalPopupInfoComponent } from '../modal-popup-info/modal-popup-info.component';

@Component({
  selector: 'app-manage-config',
  templateUrl: './manage-config.component.html',
  styleUrls: ['./manage-config.component.css']
})

export class ManageConfigComponent {

  configs: IConfigurationDetail[];
  page: number = 1;
  pageSize: number = 5;
  isUnauthorized: boolean = false;
  isLoaded: boolean = false;
  alert: ModalMessage = { message: '', type: '' };

  constructor(
    private _ConfigService: ConfigurationService,
    private router: Router,
    private modalService: NgbModal
  ) {
    this.configs = [];
  }

  ngOnInit() {

    // TODO:v loads users every time from db; must do some client side caching
    this._ConfigService.getAllConfigurations();

    this._ConfigService.allConfigurations$
      .subscribe((result: IConfigurationDetail[]) => {
        this.configs = result;
        this.isLoaded = true;
      },
        error => {
          console.log(error)
          if (error.status === 403) {
            this.isUnauthorized = true;
          }
        })
      .add(() => {
        this.isLoaded = true;
      });
  }


  addNewConfig() {
    // route to new config page
    this.router.navigate(["/create-config"]);
  }

  editConfig(config: IConfigurationDetail) {
    // route to edit config page
    console.log(`edit mode: ${config.configGUID}`)
    const navigationExtras: NavigationExtras = { state: { id: config.configGUID } };
    this.router.navigate(["/edit-config"], navigationExtras);
  }

  detailsConfig(config: IConfigurationDetail) {
    // route to details config page
    console.log(`details mode: ${config.configGUID}`)

  }

  deleteConfig(config: IConfigurationDetail) {

    this._ConfigService.deleteConfiguration(config)
      .subscribe(
        (data: IDbResponse) => {
          if (data.isSuccess) {
            this._ConfigService.getAllConfigurations();
            this.alert = { title: 'Success', message: 'Deleted Successfully', type: 'success' };
          }
          else {
            this.alert = { title: 'Failed', message: 'Deletion Failed. ' + data.message, type: 'warning' };
          }
        },
        error => {
          this.alert = { title: 'Error', message: 'Some error occured.', type: 'danger' };
        })
      .add(() => {
        const modalRef = this.modalService.open(ModalPopupInfoComponent);
        modalRef.componentInstance.alert = this.alert;
      });
  }

}
