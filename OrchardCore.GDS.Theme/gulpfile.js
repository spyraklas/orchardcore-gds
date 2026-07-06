/// <binding BeforeBuild='build' />
const { src, dest, series, parallel } = require('gulp');
const rename = require('gulp-rename');
const uglify = require('gulp-uglify');
const sass = require('gulp-sass')(require('sass'));
const sourcemaps = require('gulp-sourcemaps');
const csso = require('gulp-csso');
const replace = require('gulp-replace');
const del = require('del');

// Clean the wwwroot folder
function clean() {
    var del = require('del');
    return del(['./wwwroot/**', '!./wwwroot/Theme.png', '!./wwwroot/favicon.ico']);
}

// GDS files
function GdsAssets() {
    return src('./node_modules/govuk-frontend/dist/govuk/assets/**')
        .pipe(dest('./wwwroot/assets/'));
}

function GdsScripts() {
    return src('./node_modules/govuk-frontend/dist/govuk/all.bundle.js')
        .pipe(rename('govuk-frontend.js'))
        .pipe(dest('./wwwroot/js/'))
        .pipe(uglify())
        .pipe(rename(function (path) {
            return {
                dirname: path.dirname,
                basename: 'govuk-frontend',
                extname: '.min.js'
            }
        }))
        .pipe(dest('./wwwroot/js/'));
}

function GdsStyles() {
    return src('./node_modules/govuk-frontend/dist/govuk/index.scss')
        .pipe(sourcemaps.init())
        .pipe(sass())
        .pipe(sourcemaps.write())
        .pipe(replace(/assets\//g, 'gdscontents/assets/'))
        .pipe(rename('govuk-frontend.css'))
        .pipe(dest('./wwwroot/css/',))
        .pipe(csso({ restructure: false }))
        .pipe(rename(function (path) {
            return {
                dirname: path.dirname,
                basename: 'govuk-frontend',
                extname: '.min.css'
            }
        }))
        .pipe(dest('./wwwroot/css/'));
}

// jQuery
function jquery() {
    return src('./node_modules/jquery/dist/*.js')
        .pipe(dest('./wwwroot/js/'));
}

//site pack files
function assets() {
    return src('./pack/assets/**')
        .pipe(dest('./wwwroot/assets/'));
}

function styles() {
    return src('./pack/src/scss/*.scss')
        .pipe(sourcemaps.init())
        .pipe(sass())
        .pipe(sourcemaps.write())
        .pipe(dest('./wwwroot/css/'))
        .pipe(csso({ restructure: false }))
        .pipe(rename(function (path) {
            return {
                dirname: path.dirname,
                basename: path.basename,
                extname: '.min.css'
            }
        }))
        .pipe(dest('./wwwroot/css/'));
};

function scripts() {
    return src('./pack/src/js/*.js')
        .pipe(dest('./wwwroot/js/'))
        .pipe(uglify())
        .pipe(rename({ extname: '.min.js' }))
        .pipe(dest('./wwwroot/js/'));
}

exports.clean = clean;
exports.build = series(clean, GdsAssets, GdsStyles, GdsScripts, jquery, assets, parallel(styles, scripts));