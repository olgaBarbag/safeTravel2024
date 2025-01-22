import { Gender } from "../enums/gender.enum";
import { UserRole } from "../enums/user-role.enum";

export class UserCitizenDto {
    id!: number;
    firstName!: string;
    lastName!: string;
    email!: string;
    phoneNumber!: string;
    occupation!: string;
    country!: string;
    city!: string;
    address!: string;
    postalCode!: string;
    userRole!: UserRole;
    gender!: Gender;
    birthDate!: Date;
    username!: string;
    password!: string;
  }