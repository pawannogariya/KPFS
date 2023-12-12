export class IRegisterUserDto {
  public firstName : string='';
  public lastName: string='';
  public email:string='';
  public password:string='';
 
  public constructor() {
  }
}

export class IAddUserDto extends IRegisterUserDto {
  public role : string='';
}