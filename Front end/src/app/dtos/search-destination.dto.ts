import { DestinationType } from "../enums/destination-type.enum";

export class SearchDestinationDto {
    id!: number;
    country!: string;
    city!: string;
    region!: string;
    destinationType!: DestinationType;
  }