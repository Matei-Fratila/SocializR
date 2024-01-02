export interface LoginRequest {
    email: string,
    password: string
}

export interface RegisterRequest {
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    birthDate: Date
}