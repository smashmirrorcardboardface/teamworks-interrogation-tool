`<template>
    <div style="margin-top:10px; float:left;">
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #0990ac; color:white;">
            <h2 class="panel-title" >Ticket# {{ ticket.ticketReference }} - {{ticket.categoryDescription}} / {{ticket.subCategoryDescription}}</h2>
            </div>
            <div class="panel-body">

            <table class="table">
                <tr>
                    <td>Create Date: {{ ticket.createDate }}</td>
                    <td>Closed Date: {{ ticket.closedDate }}</td>
                </tr>
                <tr>
                    <td>Centre: {{ ticket.centreName }}</td>
                    <td>Created By: {{ ticket.createUser }}</td>
                </tr>
                <tr>
                    <td>Company Name: {{ ticket.companyName?ticket.companyName:ticket.requesterName }}</td>
                    <td>Contact Name: {{ ticket.contactName }}</td>
                </tr>
                <tr>
                    <td>Titan Company ID: {{ ticket.titanCompanyId }}</td>
                    <td>Contact Email: {{ ticket.contactEmail }}</td>
                </tr>
            </table>
            </div>
              <!-- Table -->

        </div>
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #0990ac; color:white;">
            <h2 class="panel-title">Comments</h2>
            </div>
            <div style="max-height: 400px; overflow-y: scroll;">
                <p v-if="ticketComments.length==0"><em>Loading comments...</em></p>
                <ul class="list-group">
                    <li class="list-group-item" v-for="comment in ticketComments" :key="comment.commetId">
                        <h6>Created: {{comment.createDate}} by {{comment.createUser}}</h6>
                        {{comment.comment}}
                    </li>
                </ul>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #0990ac; color:white;">
            <h2 class="panel-title">Documents</h2>
            </div>
            <div style="max-height: 300px; min-height:100px; overflow-y: scroll;">
                    <div v-for="ticketDocument in ticketDocuments" :key="ticketDocument.blobName" style=" font-size: 1.2em; padding:6px; margin-left:10px; width:50%">
                        <div style="float:left">{{ticketDocument.blobName}}</div>
                        <a style="float:right; cursor:pointer;" :href="'/api/File/Download?blobName='+ticketDocument.blobName">Download <span class='glyphicon glyphicon-download'></span></a>
                    </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
  props: { ticketReference: { type: Number } },
  data() {
    return {
      ticket: {},
      ticketComments: [],
      ticketDocuments: []
    };
  },
  async mounted() {
    console.log(this.ticketReference);
    try {
      let response = await this.$http.get(
        'api/Ticket/GetTop100Tickets?ticketReference=' + this.ticketReference
      );
      console.log(response.data);
      this.ticket = response.data[0];
    } catch (error) {
      console.log(error);
    }
    try {
      let response = await this.$http.get(
        'api/Ticket/GetTicketComments?ticketReference=' + this.ticketReference
      );
      console.log('Comment response: ', response.data);
      this.ticketComments = response.data;
    } catch (error) {
      console.log(error);
    }
    try {
      let response = await this.$http.get(
        'api/File/GetBlobListAsync?ticketReference=' + this.ticketReference
      );
      console.log('Document response: ', response.data);
      this.ticketDocuments = response.data;
    } catch (error) {
      console.log(error);
    }
  }
};
</script>

<style>
</style>
`
