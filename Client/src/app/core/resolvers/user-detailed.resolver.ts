import { ResolveFn } from "@angular/router";
import { UserData } from "src/app/models/userData";
import { AdminService } from "../services/admin.service";
import { inject } from "@angular/core";


export const UserDetailedResolver: ResolveFn<UserData> = (route, state) => {
    const adminService = inject(AdminService);
    return adminService.getUser(route.paramMap.get('id')!);
};
