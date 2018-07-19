import FetchData from 'components/fetch-data';
import TicketDetail from 'components/ticket-Detail';

export const routes = [
  {
    path: '/',
    component: FetchData,
    display: 'Ticket Data',
    style: 'glyphicon glyphicon-home'
  },
  {
    name: 'fetchData',
    path: '/fetch-data',
    component: FetchData,
    display: 'Ticket Data',
    style: 'glyphicon glyphicon-th-list'
  },
  {
    name: 'ticketDetail',
    path: '/ticket-detail',
    component: TicketDetail,
    props: true
  }
];
