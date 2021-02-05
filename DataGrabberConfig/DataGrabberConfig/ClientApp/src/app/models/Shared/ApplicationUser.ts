
export class ApplicationUser {

  constructor(UserName :string) {
    this.userName = UserName;
  }

  displayUserGUID?: string;
  role?: string;
  userName?: string;
  password?: string;
  emailAddress?: string;
  firstName?: string;
  lastName?: string;
  fullName?: string;

  tokenExpiry?: number;
}
