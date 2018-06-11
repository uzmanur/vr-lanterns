# python script to create lantern object in Blender

import bpy
import math

r = 1
h = 6*r

# paper
bpy.ops.mesh.primitive_cylinder_add(vertices = 64, location = (0,0,0))
paper = bpy.context.active_object
paper.scale = [r,r,h/4] # cylinder has radius = 2 and height = 3
bpy.ops.object.mode_set(mode = 'EDIT') 
bpy.ops.mesh.select_mode(type= 'VERT')
bpy.ops.mesh.select_all(action = 'DESELECT')
bpy.ops.object.mode_set(mode = 'OBJECT')
bpy.ops.object.shade_smooth()
for i in range(0,64):
    paper.data.vertices[2*i].select = True
bpy.ops.object.mode_set(mode = 'EDIT')
bpy.ops.mesh.delete(type='FACE') # opens bottom of cylinder
bpy.ops.mesh.select_all(action = 'SELECT')
bpy.ops.mesh.extrude_region_shrink_fatten(TRANSFORM_OT_shrink_fatten={"value":0.001}) # extrudes "paper" mesh so both sides can display material in Unity

# wires
len = r - 0.25*r*math.sqrt(2) # length of wire = dist b/t rim and wick
mv = len/2 + 0.25*r*math.sqrt(2)
xMove = [0,0,mv,-mv]
yMove = [mv,-mv,0,0]
xRotate = [90,90,0,0]
yRotate = [0,0,90,90]
bpy.ops.object.mode_set(mode = 'OBJECT')
for i in range(4):
    bpy.ops.object.select_all(action='DESELECT')
    bpy.ops.mesh.primitive_cylinder_add(location = (xMove[i], yMove[i], -h/4))
    bpy.ops.object.mode_set(mode = 'OBJECT')
    bpy.ops.object.shade_smooth()
    string = bpy.context.active_object
    string.scale = [0.02*r, 0.02*r, len/2]
    string.rotation_euler = [xRotate[i]*math.pi/180, yRotate[i]*math.pi/180, 0]

# wick
bpy.ops.object.select_all(action='DESELECT')
bpy.ops.mesh.primitive_torus_add(location = (0,0,-h/4), major_segments = 64, minor_segments = 64, major_radius = 0.25*r*math.sqrt(2) - 0.1*r, minor_radius = 0.1*r)
wick = bpy.context.active_object
bpy.ops.object.shade_smooth()
bpy.ops.object.mode_set(mode = 'EDIT')
bpy.ops.mesh.extrude_region_shrink_fatten(TRANSFORM_OT_shrink_fatten={"value":0.001})
bpy.ops.object.mode_set(mode = 'OBJECT')

# rim
bpy.ops.object.select_all(action='DESELECT')
bpy.ops.mesh.primitive_torus_add(location = (0,0,-h/4), major_radius = r, minor_radius = 0.04*r)
rim = bpy.context.active_object
bpy.ops.object.shade_smooth()

bpy.ops.object.select_all(action='DESELECT')
