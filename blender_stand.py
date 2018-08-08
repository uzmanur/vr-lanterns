# Python script to create mesh for lantern stand in Blender

import bpy
import math

r = 0.25
h = 2

bpy.ops.mesh.primitive_cylinder_add(vertices = 64, location = (0,0,0))
bpy.ops.object.shade_smooth()
stand = bpy.context.active_object
stand.name = "Stand"
stand.scale = [r,r,h] # cylinder has radius = 1 and height = 4
bpy.ops.object.mode_set(mode = 'EDIT') 
bpy.ops.mesh.select_mode(type= 'VERT')
bpy.ops.mesh.select_all(action = 'DESELECT')

# subdivides cylinder into 32 subdivisions along vertical axis
for i in range(0,64):
    bpy.ops.object.mode_set(mode = 'OBJECT')
    stand.data.vertices[2*i].select = True
    stand.data.vertices[2*i+1].select = True
    bpy.ops.object.mode_set(mode = 'EDIT')
    bpy.ops.mesh.subdivide(number_cuts=31, quadcorner='PATH')
    bpy.ops.mesh.select_all(action = 'DESELECT')

# resizes top of cylinder to create pedestal of stand
for i in range(0,64):
    bpy.ops.object.mode_set(mode = 'OBJECT')
    stand.data.vertices[2*i].select = True
bpy.ops.object.mode_set(mode = 'EDIT')
bpy.ops.transform.resize(value=(2,2,2), constraint_axis=(False, False, False), constraint_orientation='NORMAL', mirror=False, proportional='ENABLED', proportional_edit_falloff='SMOOTH', proportional_size=0.5)

bpy.ops.mesh.select_all(action = 'DESELECT')

# resizes bottom of cylinder to create base of stand
for i in range(0,64):
    bpy.ops.object.mode_set(mode = 'OBJECT')
    stand.data.vertices[2*i+1].select = True
bpy.ops.object.mode_set(mode = 'EDIT')
bpy.ops.transform.resize(value=(2,2,2), constraint_axis=(False, False, False), constraint_orientation='NORMAL', mirror=False, proportional='ENABLED', proportional_edit_falloff='SMOOTH', proportional_size=0.5)
