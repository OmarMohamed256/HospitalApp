import { ResolveFn } from "@angular/router";
import { UserData } from "src/app/models/userData";
import { UserService } from "../services/user.service";
import { inject } from "@angular/core";


export const UserDetailedResolver: ResolveFn<UserData> = (route, state) => {
    const userService = inject(UserService);
    return userService.getUser(route.paramMap.get('id')!);
};
