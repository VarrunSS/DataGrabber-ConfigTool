import { Injectable } from '@angular/core';

// Usage
//this._LoggerService.log(decodedToken);

@Injectable({ providedIn: 'root' })
export class LoggerService {
  logs: string[] = []; // capture logs for testing


  constructor() {

  }

  log(message: string) {
    this.logs.push(message);
    console.log(message);
  }
}
