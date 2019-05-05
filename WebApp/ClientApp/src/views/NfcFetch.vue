<template>
  <v-container fluid>
    <v-slide-y-transition mode="out-in">
      <v-layout column>
        <h1>NFC fetch</h1>
        <p>This component demonstrates fetching data from the server.</p>

        <v-data-table
            :headers="headers"
            :items="scans"
            hide-actions
            :loading="loading"
            class="elevation-1"
          >
            <v-progress-linear v-slot:progress color="blue" indeterminate></v-progress-linear>
            <template v-slot:items="props">
              <td>{{ props.item.scanId }}</td>
              <td>{{ props.item.uid }}</td>
              <td>{{ props.item.timeStamp }}</td>
              <td>{{ props.item.deviceId }}</td>
            </template>
          </v-data-table>

      </v-layout>
    </v-slide-y-transition>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { NfcScan } from '../models/NfcScan';
import axios from 'axios';
import '../components/login/auth-header';
import { authHeader } from '../components/login/auth-header';

@Component({})
export default class FetchDataView extends Vue {
  private loading: boolean = true;
  private scans: NfcScan[] = [];
  private headers = [
    { text: 'Skenavimo Id', value: 'scanId' },
    { text: 'UID', value: 'uid' },
    { text: 'Data', value: 'dateTime' },
    { text: 'Įrenginio Id', value: 'deviceId' },
  ];

  private created() {
    this.fetcNfc();
  }

  private fetcNfc() {
    const headers = {...authHeader()};
    axios.get<NfcScan[]>('api/nfcscan/getscans', {headers:headers})
      .then((response) => {
        this.scans = response.data;
        this.loading = false;
      });
  }
}
</script>
