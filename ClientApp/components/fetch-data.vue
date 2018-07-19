<template>
    <div>
        <p v-if="!tickets"><em>Loading...</em></p>
        <table class="table" v-if="tickets">
            <thead>
                <tr>
                    <th>Ticket Reference</th>
                    <th>Created</th>
                    <th>Logged By</th>
                    <th>Company</th>
                    <th>Centre</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="ticket in tickets" :key="ticket.ticketReference">
                    <td>{{ ticket.ticketReference }}</td>
                    <td>{{ ticket.createDate }}</td>
                    <td>{{ ticket.createUser }}</td>
                    <td>{{ ticket.companyName }}</td>
                    <td>{{ticket.centreName}}</td>
                    <td><button v-on:click="openDetail(ticket.ticketReference)" class="btn btn-secondary"><span class='glyphicon glyphicon-search'></span></button></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import router from '../router.js';

export default {
  props: { tickets: { type: Array } },
  data() {
    return {
      ticketDetail: null,
      ticketComments: null
    };
  },
  methods: {
    async openDetail(ticketReference) {
      router.push({
        name: 'ticketDetail',
        params: { ticketReference: ticketReference }
      });
    }
  }
};
</script>

<style>
</style>
