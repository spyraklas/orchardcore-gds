/// <binding BeforeBuild='build' />
const { src, dest, series, parallel } = require('gulp');
const rename = require('gulp-rename');
const uglify = require('gulp-uglify');
const sass = require('gulp-sass')(require('sass'));
const sourcemaps = require('gulp-sourcemaps');
const csso = require('gulp-csso');
const replace = require('gulp-replace');
const cleanCSS = require('gulp-clean-css');
const del = require('del');

// Clean the wwwroot folder
function clean() {
    var del = require('del');
    return del(['./wwwroot/**', '!./wwwroot/Theme.png', '!./wwwroot/favicon.ico']);
}

// GDS files
function GdsAssets() {
    return src('./node_modules/govuk-frontend/dist/govuk/assets/**', { encoding: false })
        .pipe(dest('./wwwroot/assets/'));
}

function GdsScripts() {
    return src('./node_modules/govuk-frontend/dist/govuk/all.bundle.js', { encoding: false })
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
    return src('./node_modules/govuk-frontend/dist/govuk/index.scss', { encoding: false })
        .pipe(sourcemaps.init())
        .pipe(sass())
        .pipe(sourcemaps.write())
        .pipe(replace(/assets\//g, 'OrchardCore.GDS.NeetTheme/assets/'))
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
    return src('./node_modules/jquery/dist/*.js', { encoding: false })
        .pipe(dest('./wwwroot/js/'));
}

//site pack files
function assets() {
    return src('./pack/assets/**', { encoding: false })
        .pipe(dest('./wwwroot/assets/'));
}

function styles() {
    return src('./pack/src/scss/*.scss', { encoding: false })
        .pipe(sourcemaps.init())
        .pipe(sass())
        .pipe(sourcemaps.write())
        .pipe(replace(/public\//g, 'OrchardCore.GDS.NeetTheme/assets/'))
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
}

function neetstyles() {
    return src('./pack/css/*.css', { encoding: false })
        .pipe(dest('./wwwroot/css/'))
        .pipe(cleanCSS())
        .pipe(rename(function (path) {
            return {
                dirname: path.dirname,
                basename: path.basename,
                extname: '.min.css'
            }
        }))
        .pipe(dest('./wwwroot/css/'));
}

function scripts() {
    return src('./pack/src/js/*.js', { encoding: false })
        .pipe(dest('./wwwroot/js/'))
        .pipe(uglify())
        .pipe(rename({ extname: '.min.js' }))
        .pipe(dest('./wwwroot/js/'));
}

exports.clean = clean;
exports.build = series(clean, GdsAssets, GdsStyles, GdsScripts, jquery, assets, parallel(styles, neetstyles, scripts));