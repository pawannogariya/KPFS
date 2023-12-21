import { IResponse } from "@app/_services/dto/response.dto";
import { Role } from "./role";

export interface User {
    id: number;
    firstName: string;
    lastName: string;
    username: string;
    role: Role;
    token?: string;
}
export interface UserWithToken{
    token?: string;
    expiration?:string;
    user:User;
}

export interface IUsers {
    email: string
    emailConfirmed: boolean
    firstName: string
    id: string
    isActive: boolean
    lastName: string
    role: string
  }