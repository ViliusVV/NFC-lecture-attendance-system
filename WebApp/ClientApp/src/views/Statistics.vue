<template>
<v-layout>
    <v-flex xs12 sm4 offset-sm3>
      <v-card width="450"   class="elevation-4">
        <v-card-title primary-title>
            <h3 align="center" class="headline mb-0">Kazkieno lankomumas</h3>
        </v-card-title>
          <div>  
            <apexchart type=radialBar height=350 :options="chartOptions" :series="series" /> 
          </div>
      </v-card>
    </v-flex>
  </v-layout>
</template>



<script lang="js">
import { Component, Vue } from 'vue-property-decorator';
import axios from 'axios';
import '../components/login/auth-header';
import { authHeader } from '../components/login/auth-header';
import VueApexCharts from 'vue-apexcharts'

Vue.component('apexchart', VueApexCharts)

export default({
  data: function() {
    return {
      series: [80, 60, 100, 10],
      chartOptions: {
        plotOptions: {
          radialBar: {
            offsetY: -10,
            startAngle: 0,
            endAngle: 270,
            hollow: {
              margin: 5,
              size: '30%',
              background: 'transparent',
              image: undefined,
            },
            dataLabels: {
              name: {
                show: false,

              },
              value: {
                show: true,
              }
            }
          }
        },
        colors: ['#1ab7ea', '#0084ff', '#39539E', '#0077B5'],
        labels: ['Iš viso', 'Laboratorinai', 'Pratybos', 'Teorija'],
        legend: {
          show: true,
          floating: true,
          fontSize: '16px',
          position: 'left',
          offsetX: 35,
          offsetY: 10,
          labels: {
              useSeriesColors: true,
          },
          markers: {
              size: 0
          },
          formatter: function(seriesName, opts) {
              return seriesName + ":  " + opts.w.globals.series[opts.seriesIndex] + "%"
          },
          itemMargin: {
              horizontal: 1,
          }
        },
        responsive: [{
              breakpoint: 480,
              options: {
                  legend: {
                      show: false
                  }
              }
          }]
      }
    }
  }
});

</script>
