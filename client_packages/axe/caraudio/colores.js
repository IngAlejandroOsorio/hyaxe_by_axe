var hue_slider_picker = document.getElementById("hue_slider_picker");
var brightness_slider_picker = document.getElementById(
  "brightness_slider_picker"
);
var saturation_slider_picker = document.getElementById(
  "saturation_slider_picker"
);

var elem1 = document.getElementById("color_picker_bg");
var rect = elem1.getBBox();
var rect_x = rect.x + rect.width / 2;
var rect_y = rect.y + rect.height / 2;

console.log(rect_x);
console.log(rect_y);

hue_slider_picker.setAttribute("transform", `rotate(306,${rect_x},${rect_y})`);
saturation_slider_picker.setAttribute(
  "transform",
  `rotate(336,${rect_x},${rect_y})`
);
brightness_slider_picker.setAttribute(
  "transform",
  `rotate(253,${rect_x},${rect_y})`
);

function hsb_to_hsl(h, s, b) {
  // both hsb and hsl values are in [0, 1]
  var l = (2 - s) * b / 2;
  if (l != 0) {
    if (l == 1) {
      s = 0;
    } else if (l < 0.5) {
      s = s * b / (l * 2);
    } else {
      s = s * b / (2 - l * 2);
    }
  }
  return [h, s, l];
}

var hsl_h = 205;
var hsl_s = 0.82;
var hsl_l = 0.65;

var hsb_s = 0.81;
var hsb_b = 0.92;
//console.log(hsb_to_hsl(hsl_h, hsb_s, hsb_b)); // to find hsl values.

function set_item_rotation(rotationDeg, slider) {
  // var rotationDeg = Math.round(((rotationRad * (180/Math.PI))+180)); // removed %360
  var rotationMax = parseInt(slider.getAttribute("data-maxr"));
  var rotationMin = parseInt(slider.getAttribute("data-minr"));
  var rotationDiff = rotationMax - rotationMin;

  if (rotationDeg > rotationMax) {
    rotationDeg = rotationMax;
  } else if (rotationDeg < rotationMin) {
    rotationDeg = rotationMin;
  }

  var rotationRatio =
    (rotationDiff - (rotationDeg - rotationMin)) / rotationDiff;

  var sliderType = slider.getAttribute("data-hsb");
  if (sliderType == "h") {
    hsl_h = Math.round(rotationRatio * 360);
  } else if (sliderType == "s") {
    hsb_s = rotationRatio;
  } else if (sliderType == "b") {
    hsb_b = rotationRatio;
  }
  var hsl_arr = hsb_to_hsl(hsl_h, hsb_s, hsb_b);
  hsl_arr[1] = (hsl_arr[1] * 100).toFixed(0);
  hsl_arr[2] = (hsl_arr[2] * 100).toFixed(0);
  //console.log(hsl_arr);

  var hsl_arr_sat_min = hsb_to_hsl(hsl_h, 0, hsb_b);
  hsl_arr_sat_min[1] = (hsl_arr_sat_min[1] * 100).toFixed(0);
  hsl_arr_sat_min[2] = (hsl_arr_sat_min[2] * 100).toFixed(0);

  var hsl_arr_sat_max = hsb_to_hsl(hsl_h, 1, hsb_b);
  hsl_arr_sat_max[1] = (hsl_arr_sat_max[1] * 100).toFixed(0);
  hsl_arr_sat_max[2] = (hsl_arr_sat_max[2] * 100).toFixed(0);

  var hsl_arr_bright_min = hsb_to_hsl(hsl_h, hsb_s, 0);
  hsl_arr_bright_min[1] = (hsl_arr_bright_min[1] * 100).toFixed(0);
  hsl_arr_bright_min[2] = (hsl_arr_bright_min[2] * 100).toFixed(0);

  var hsl_arr_bright_max = hsb_to_hsl(hsl_h, hsb_s, 1);
  hsl_arr_bright_max[1] = (hsl_arr_bright_max[1] * 100).toFixed(0);
  hsl_arr_bright_max[2] = (hsl_arr_bright_max[2] * 100).toFixed(0);

  document.getElementById("selected_color_shape").style.fill = `hsl(${
    hsl_arr[0]
  },${hsl_arr[1]}%,${hsl_arr[2]}%)`;
  slider.setAttribute(
    "transform",
    `rotate(${rotationDeg},${rect_x},${rect_y})`
  );

  document
    .getElementById("sat_stop_color_1")
    .setAttribute(
      "stop-color",
      `hsl(${hsl_arr[0]},${hsl_arr_sat_min[1]}%,${hsl_arr_sat_min[2]}%)`
    );
  document
    .getElementById("sat_stop_color_2")
    .setAttribute(
      "stop-color",
      `hsl(${hsl_arr[0]},${hsl_arr_sat_max[1]}%,${hsl_arr_sat_max[2]}%)`
    );

  document
    .getElementById("bright_stop_color_1")
    .setAttribute(
      "stop-color",
      `hsl(${hsl_arr[0]},${hsl_arr_bright_min[1]}%,${hsl_arr_bright_min[2]}%)`
    );
  document
    .getElementById("bright_stop_color_2")
    .setAttribute(
      "stop-color",
      `hsl(${hsl_arr[0]},${hsl_arr_bright_max[1]}%,${hsl_arr_bright_max[2]}%)`
    );

  //console.log(rotationDeg);
}

// GETTING THE CORRECT CURSOR LOCATION ON SVG
// Find your root SVG element
var svg = document.querySelector("svg");

// Create an SVGPoint for future math
var pt = svg.createSVGPoint();

// Get point in global SVG space
function cursorPoint(evt) {
  if (evt){
  pt.x = evt.clientX;
  pt.y = evt.clientY;
  if (isNaN(pt.x)) {
    pt.x = Math.round(evt.touches[0].clientX);
    pt.y = Math.round(evt.touches[0].clientY);
  }
  return pt.matrixTransform(svg.getScreenCTM().inverse());
  }
}
// END OF GETTING THE CORRECT CURSOR LOCATION ON SVG

function get_mouse_angle(event, center_elem) {
  var center_elem_rect = center_elem.getBBox();
  var central_pos = [
    center_elem_rect.x + center_elem_rect.width / 2,
    center_elem_rect.y + center_elem_rect.height / 2
  ];
  var cursor = [cursorPoint(event).x, cursorPoint(event).y];

  var rad = Math.atan2(cursor[1] - central_pos[1], cursor[0] - central_pos[0]);
  rad += Math.PI / 2;
  var deg = Math.round(rad * (180 / Math.PI) + 180);
  // adding 90 degrees for some reason.
  // returns angle in radians
  //console.log(deg);
  return deg;
}

var slider_being_dragged = null;
var hsb_sliders_last_angle = [306, 336, 253];
var mouse_slider_angle_diff = 0;
var drag_on_picker = false; // user can press either directly on slider picker or the slider bar.
var slider_index = NaN;

var lastMove = null;

function start_dragging(e) {
  var mouse_angle = get_mouse_angle(
    e,
    document.getElementById("color_picker_bg")
  );

  // Checking first if user pressed on the progress bar, not the slider.
  if (e.currentTarget.getAttribute("data-slider-target")) {
    slider_being_dragged = document.getElementById(
      e.currentTarget.getAttribute("data-slider-target")
    );
    slider_index = parseInt(
      slider_being_dragged.getAttribute("data-slider-index")
    );
    drag_on_picker = false;
  } else {
    slider_being_dragged = e.currentTarget;
    slider_index = parseInt(
      slider_being_dragged.getAttribute("data-slider-index")
    );
    drag_on_picker = true;
    mouse_slider_angle_diff =
      hsb_sliders_last_angle[slider_index] - mouse_angle;
  }
  console.log(slider_being_dragged);
  e.preventDefault();
  e.stopPropagation();

  console.log("Angle diff");
  console.log(mouse_slider_angle_diff);
  // console.log(hsb_sliders_last_angle[slider_index]);
  // console.log(hsb_sliders_last_angle[slider_index]);
  // console.log(mouse_angle);

  // var mouse_angle = get_mouse_angle(e, document.getElementById('color_picker_bg'));
  // set_rotations(mouse_angle, slider_being_dragged);
}

function stop_dragging(e) {
  if (slider_being_dragged) {
  var slider_index = parseInt(
    slider_being_dragged.getAttribute("data-slider-index")
  );}else{var slider_index = 0}
  hsb_sliders_last_angle[slider_index] =
    get_mouse_angle(lastMove, document.getElementById("color_picker_bg")) +
    mouse_slider_angle_diff;

  //console.log(slider_being_dragged);
  //console.log(slider_being_dragged.getAttribute("id"));
 // console.log(slider_index);

  slider_being_dragged = null;
  drag_on_picker = false;
  mouse_slider_angle_diff = 0;
  slider_index = NaN;
}

function drag_rotate(e) {
  if (!slider_being_dragged) {
    return;
  }
  var picker_angle = get_mouse_angle(
    e,
    document.getElementById("color_picker_bg")
  );

  if (drag_on_picker) {
    picker_angle += mouse_slider_angle_diff;
  }

  set_item_rotation(picker_angle, slider_being_dragged);
  lastMove = e;
}

function showNoFeatureAlert(e) {
  alert("This feature hasn't been implemented yet.");
}

function set_event_listeners() {
  var hue_slider = document.getElementById("hue_slider_picker");

  hue_slider.addEventListener("mousedown", start_dragging);
  document.addEventListener("mouseup", stop_dragging);
  document.addEventListener("mousemove", drag_rotate);

  hue_slider.addEventListener("touchstart", start_dragging);
  document.addEventListener("touchend", stop_dragging);
  document.addEventListener("touchmove", drag_rotate);

  var brightness_slider = document.getElementById("brightness_slider_picker");

  brightness_slider.addEventListener("mousedown", start_dragging);
  document.addEventListener("mouseup", stop_dragging);
  document.addEventListener("mousemove", drag_rotate);

  brightness_slider.addEventListener("touchstart", start_dragging);
  document.addEventListener("touchend", stop_dragging);
  document.addEventListener("touchmove", drag_rotate);

  var saturation_slider = document.getElementById("saturation_slider_picker");
  saturation_slider.addEventListener("mousedown", start_dragging);
  saturation_slider.addEventListener("touchstart", start_dragging);

  var hue_bar_svg = document.getElementById("hue_bar");
  hue_bar_svg.addEventListener("mousedown", start_dragging);
  hue_bar_svg.addEventListener("touchstart", start_dragging);

  var saturation_bar_svg = document.getElementById("saturation_bar");
  saturation_bar_svg.addEventListener("mousedown", start_dragging);
  saturation_bar_svg.addEventListener("touchstart", start_dragging);

  var brightness_bar_svg = document.getElementById("brightness_bar");
  brightness_bar_svg.addEventListener("mousedown", start_dragging);
  brightness_bar_svg.addEventListener("touchstart", start_dragging);

  var zoom_button_svg = document.getElementById("zoom_button");
}

set_event_listeners();