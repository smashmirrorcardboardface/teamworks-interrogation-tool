<template>
    <div id="app" class="container">
        <div class="row">
            <div class="col-sm-3">
                <nav-menu params="route: route" v-on:reloadData="reloadData"></nav-menu>
            </div>
            <div class="col-sm-9">
                <router-view :tickets="tickets"></router-view>
            </div>
        </div>

    </div>

</template>

<script>
import Vue from 'vue';
import FetchData from './fetch-data';
import NavMenu from './nav-menu';
import TicketDetail from './ticket-Detail';

Vue.component('fetch-data', FetchData);
Vue.component('nav-menu', NavMenu);
Vue.component('ticket-detail', TicketDetail);

export default {
  data() {
    return {
      tickets: null
    };
  },
  methods: {
    async reloadData(queryString) {
      try {
        let response = await this.$http.get(
          'api/Ticket/GetTop100Tickets?' + queryString
        );
        console.log(response.data);
        this.tickets = response.data;
      } catch (error) {
        console.log(error);
      }
    }
  },

  async created() {
    // ES2017 async/await syntax via babel-plugin-transform-async-to-generator
    // TypeScript can also transpile async/await down to ES5
    try {
      let response = await this.$http.get('api/Ticket/GetTop100Tickets');
      console.log(response.data);
      this.tickets = response.data;
    } catch (error) {
      console.log(error);
    }
    // Old promise-based approach
    //this.$http
    //    .get('/api/SampleData/WeatherForecasts')
    //    .then(response => {
    //        console.log(response.data)
    //        this.forecasts = response.data
    //    })
    //    .catch((error) => console.log(error))*/
  }
};
</script>

<style>
</style>
