export interface JwtPayload {
    nameidentifier: number;
    name: string;
    emailaddress: string;
    role: string;
    [key: string]: any; // Allows additional claims
}
