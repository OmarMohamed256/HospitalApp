import { ROLES } from 'src/app/constants/roles';
import { IRoleNavData } from 'src/app/models/role-nav-data';

export const navItems: IRoleNavData[] = [
  {
    name: 'Dashboard',
    url: '/',
    iconComponent: { name: 'cil-speedometer' },
  },

  {
    name: 'Appointments',
    url: '/appointments',
    iconComponent: { name: 'cil-calendar' },
    roles: [ROLES.ADMIN, ROLES.RECEPTIONIST, ROLES.DOCTOR]
  },
  {
    name: 'Patients',
    url: '/patients',
    iconComponent: { name: 'cil-bed' },
    roles: [ROLES.ADMIN, ROLES.RECEPTIONIST]
  },
  {
    name: 'Doctors',
    url: '/doctors',
    iconComponent: { name: 'cil-user' },
    roles: [ROLES.ADMIN, ROLES.RECEPTIONIST]
  },
  {
    name: 'Doctor Info',
    dynamicUrl: (id) => `/doctors/${id}`,
    iconComponent: { name: 'cil-user' },
    roles: [ROLES.DOCTOR]
  },
  {
    name: 'Services',
    url: '/services',
    iconComponent: { name: 'cil-layers' },
    roles: [ROLES.ADMIN]
  },
  {
    name: 'Specialities',
    url: '/specialities',
    iconComponent: { name: 'cil-library' },
    roles: [ROLES.ADMIN]
  },
  {
    name: 'Inventory',
    url: '/inventory',
    iconComponent: { name: 'cil-hospital' },
    roles: [ROLES.ADMIN]
  },
  {
    name: 'Users Panel',
    url: '/users',
    iconComponent: { name: 'cil-group' },
    roles: [ROLES.ADMIN]
  },
  {
    name: 'Clinics',
    url: '/clinics',
    iconComponent: { name: 'cil-door' },
    roles: [ROLES.ADMIN, ROLES.RECEPTIONIST]
  },
  {
    name: 'Medicines',
    url: '/medicines',
    iconComponent: { name: 'cil-drink-alcohol' },
    roles: [ROLES.ADMIN]
  }
];
