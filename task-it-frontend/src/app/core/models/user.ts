export class User {
   email: string;
   name: string;
   password: string;

  public constructor(email: string, name: string, password: string) {
      this.email = email;
      this.name = name;
      this.password = password;
  }
}
