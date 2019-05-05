<template>
  <v-container fluid>
    <v-slide-y-transition mode="out-in">
      <v-layout column>
        <h1>Įrenginių sarašas</h1>
        <p>Čia galima keisti ir peržiūrėti įrenginiu parametrus</p>
        <v-data-table
            :headers="headers"
            :items="scans"
            hide-actions
            :loading="loading"
            class="elevation-3"
          >
            <v-progress-linear v-slot:progress color="blue" indeterminate></v-progress-linear>
            
            <template v-slot:items="props">
              <td>{{ props.item.deviceId }}</td>
              <td>{{ props.item.deviceIdReal }}</td>
              <td>{{ props.item.updatePending }}</td>
              <td>{{ props.item.classroomId }}</td>
              <!-- <td><EditDevice /></td> -->
              <td><EditDevice :deviceObj = "props.item"/></td>   
            </template>
          </v-data-table>

      </v-layout>
    </v-slide-y-transition>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { Device } from '../models/Device';
import { Classroom } from '../models/Classroom';
import HelloWorld from '../components/HelloWorld.vue'
import axios from 'axios';
import '../components/login/auth-header';
import { authHeader } from '../components/login/auth-header';
import EditDevice from './PopupDialogs/EditDevice.vue';

@Component({
  components: {
    EditDevice,
    HelloWorld,
  },
})
export default class FetchDataView extends Vue {
  private loading: boolean = true;
  private scans: Device[] = [];
  private headers = [
    { text: 'Sarašo nr.', value: 'deviceId' },
    { text: 'Įrenginio Id', value: 'deviceIdReal' },
    { text: 'Laukiama atnaujinimo', value: 'updatePending' },
    { text: 'Auditorijos Id', value: 'classroomId' },
  ];

  private created() {
    this.fetcNfc();
  }

  private fetcNfc() {
    const headers = {...authHeader()};
    axios.get<Device[]>('api/nfcscan/getdevices', {headers:headers})
      .then((response) => {
        this.scans = response.data;
        this.loading = false;
      });
  }
}
</script>
