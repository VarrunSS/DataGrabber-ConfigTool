
export interface IModalMessage {
  title?: string;
  type?: string;
  message?: string;
}



export class ModalMessage {
  constructor() {
    this.title = '';
    this.type = '';
    this.message = '';
  }
  title?: string;
  type?: string;
  message?: string;
}
