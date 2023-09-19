import { ROLES } from 'src/app/constants/roles';
import { IRoleNavData } from 'src/app/models/role-nav-data';

export const navItems: IRoleNavData[] = [
  {
    name: 'Dashboard',
    url: '/',
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW'
    },
  },
  {
    name: 'Components',
    title: true
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
    name: 'Services',
    url: '/services',
    iconComponent: { name: 'cil-layers' },
    roles: [ROLES.ADMIN, ROLES.RECEPTIONIST]
  },
  {
    name: 'Specialities',
    url: '/specialities',
    iconComponent: { name: 'cil-library' },
    roles: [ROLES.ADMIN, ROLES.RECEPTIONIST]
  },
  {
    name: 'Users Panel',
    url: '/users',
    iconComponent: { name: 'cil-group' },
    roles: [ROLES.ADMIN, ROLES.RECEPTIONIST]
  }
];
