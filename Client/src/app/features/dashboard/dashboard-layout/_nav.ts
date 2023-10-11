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
    iconComponent: { name: 'cil-calendar' }
  },
  {
    name: 'Patients',
    url: '/patients',
    iconComponent: { name: 'cil-bed' },
  },
  {
    name: 'Doctors',
    url: '/doctors',
    iconComponent: { name: 'cil-user' },
  },
  {
    name: 'Services',
    url: '/services',
    iconComponent: { name: 'cil-layers' },
  },
  {
    name: 'Specialities',
    url: '/specialities',
    iconComponent: { name: 'cil-library' },
  },
  {
    name: 'Inventory',
    url: '/inventory',
    iconComponent: { name: 'cil-hospital' },
  },
  {
    name: 'Users Panel',
    url: '/users',
    iconComponent: { name: 'cil-group' },
  },
  {
    name: 'Time Table',
    url: '/time-table',
    iconComponent: { name: 'cil-view-column' },
  },
  {
    name: 'Rooms',
    url: '/rooms',
    iconComponent: { name: 'cil-door' },
  },
];
