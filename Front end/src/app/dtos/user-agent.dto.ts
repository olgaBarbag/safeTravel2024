import { UserRole } from "../enums/user-role.enum";

export class UserAgentDto {
    id!: number;
    firstName!: string;
    lastName!: string;
    email!: string;
    phoneNumber!: string;
    companyName!: string;
    country!: string;
    city!: string;
    address!: string;
    postalCode!: string;
    userRole!: UserRole;
    username!: string;
    password!: string;
  }