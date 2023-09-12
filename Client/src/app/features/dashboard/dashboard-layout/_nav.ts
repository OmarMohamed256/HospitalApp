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
    name: 'Patient',
    url: '/patients',
    iconComponent: { name: 'cil-bed' },
    roles: [ROLES.ADMIN, ROLES.RECEPTIONIST]

  },
  
];
