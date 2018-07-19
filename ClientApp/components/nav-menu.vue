<template>
    <div class="main-nav">
        <div class="navbar">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" v-on:click="toggleCollapsed">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                <h3>Teamworks Interrogation Tool</h3>
            </div>
            <div class="clearfix"></div>
            <transition name="slide">
                <div class="navbar-collapse collapse in" v-show="!collapsed">
                    <ul class='nav navbar-nav'>
                        <li>
                        <label for="ticketTypeSelector">Ticket Type:</label>
                        <select class="form-control" id="ticketTypeSelector">
                            <option value="CS ">Customer Service Tickets</option>
                            <option value="GSC ">Global Service Centre Tickets</option>
                        </select>
                        <small>Only CSTS and GSC tickets are archived</small>
                        </li><br>
                        <li>
                        <h4>Date Range:</h4>
                        <label for="to-datetime">From:</label>
                        <datepicker :clear-button="true" type="text" v-model="fromDate" class="form-control"></datepicker>
                        <label for="to-datetime">To:</label>
                        <datepicker :clear-button="true" type="text" v-model="toDate" class="form-control"></datepicker>
                        </li><br><br>
                        <li>
                        <label for="ticketReference">Ticket Reference:</label>
                        <div class="input-group mb-3" id="ticketReference">
                            <input type="text" v-model="ticketReference" class="form-control" />
                        </div>
                        </li>
                        <li>
                        <label for="centreNumber">Centre Number:</label>
                        <div class="input-group mb-3" id="centreNumber">
                            <input type="text" v-model="locationNumber" class="form-control" />
                        </div>
                        </li>
                        <li>
                        <label for="companyId">Company Id:</label>
                        <div class="input-group" id="companyId">
                            <input type="text" v-model="titanCompanyId" class="form-control" />
                        </div>
                        </li>
                        <li>
                        <button v-on:click="filter" class="btn btn-secondary" style="margin-top:5px; float:right;">
                            <span class='glyphicon glyphicon-th-list'></span> Filter Data
                        </button>
                        </li>
                    </ul>
                </div>
            </transition>
        </div>
    </div>
</template>

<script>
import { routes } from '../routes';
import router from '../router.js';
import Datepicker from 'vuejs-datepicker';
import moment from 'moment';
export default {
  props: { tickets: { type: Array } },
  data() {
    return {
      routes,
      collapsed: true,
      companyId: null,
      ticketReference: null,
      locationNumber: null,
      titanCompanyId: null,
      fromDate: null,
      toDate: null
    };
  },
  methods: {
    customFormatter(date) {
      return moment(date).format('MMMM Do YYYY, h:mm:ss a');
    },
    toggleCollapsed: function(event) {
      this.collapsed = !this.collapsed;
    },
    filter() {
      let fromDateProper = moment(this.fromDate).format('YYYY-MM-DD');
      let toDateProper = moment(this.toDate).format('YYYY-MM-DD');
      //console.log(fromDateProper.format());
      let queryString = `titanCompanyId=${
        this.titanCompanyId
      }&ticketReference=${this.ticketReference}&locationNumber=${
        this.locationNumber
      }&toDate=${toDateProper ? toDateProper : 0}&fromDate=${
        fromDateProper ? fromDateProper : 0
      }`;
      //)}&fromDate=${encodeURIComponent(moment(this.fromDate))}`;
      console.log(queryString);
      this.$emit('reloadData', queryString);
      router.push({ name: 'fetchData' });
    }
  },
  components: {
    Datepicker
  }
};
</script>

<style>
.slide-enter-active,
.slide-leave-active {
  transition: max-height 0.35s;
}
.slide-enter,
.slide-leave-to {
  max-height: 0px;
}

.slide-enter-to,
.slide-leave {
  max-height: 20em;
}
</style>
