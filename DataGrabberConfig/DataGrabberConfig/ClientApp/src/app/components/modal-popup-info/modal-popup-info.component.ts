import { Component, Input } from '@angular/core';
import { NgbActiveModal, NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { ModalMessage } from 'src/app/models';

@Component({
  selector: 'app-modal-popup-info',
  templateUrl: './modal-popup-info.component.html',
  styleUrls: ['./modal-popup-info.component.css'],
  providers: [NgbModalConfig, NgbModal]
})

export class ModalPopupInfoComponent {
  @Input() alert: ModalMessage;


  constructor(
    public activeModal: NgbActiveModal,
    public config: NgbModalConfig) {
    // customize default values of modals used by this component tree
    config.backdrop = 'static';
    config.keyboard = false;
    config.centered = true;
  }
}
